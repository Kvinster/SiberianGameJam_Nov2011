using UnityEngine;
using UnityEngine.Assertions;

using System;

using SGJ.Utils;

namespace SGJ.Core.Bathroom {
	public sealed class Hand : MonoBehaviour {
		enum State {
			Idle,
			Chasing,
			Returning,
			Grabbed,
		}

		[Header("Parameters")]
		public float MinMovementSpeed;
		public float MaxMovementSpeed;
		public float SecondGrabMovementSpeed;
		[Header("Dependencies")]
		public BathroomLevelManager LevelManager;
		public ColliderNotifier2D TriggerNotifier;
		public Transform          StartPos;
		public Soap               Soap;

		bool _isOverSoap;

		float   _movementSpeed;
		Vector3 _targetPos;
		Vector3 _soapShift;

		State _curState = State.Idle;

		public bool IsIdle      => (_curState == State.Idle);
		public bool IsChasing   => (_curState == State.Chasing);
		public bool IsReturning => (_curState == State.Returning);
		public bool IsGrabbing  => (_curState == State.Grabbed);

		public event Action<Hand> OnGrabbedSoap;
		public event Action<Hand> OnMissedSoap;

		void Start() {
			TriggerNotifier.OnTriggerEnter += OnNotifierTriggerEnter;
			TriggerNotifier.OnTriggerExit += OnNotifierTriggerExit;
		}

		void Update() {
			switch ( _curState ) {
				case State.Idle: {
					break;
				}
				case State.Chasing: {
					if ( TryMoveToPoint(_targetPos) ) {
						if ( _isOverSoap ) {
							_curState  = State.Grabbed;
							_soapShift = transform.position - Soap.transform.position;
							OnGrabbedSoap?.Invoke(this);
						} else {
							_curState = State.Returning;
							OnMissedSoap?.Invoke(this);
						}
					}
					break;
				}
				case State.Returning: {
					if ( TryMoveToPoint(StartPos.position) ) {
						_curState = State.Idle;
					}
					break;
				}
				case State.Grabbed: {
					transform.position = Soap.transform.position + _soapShift;
					break;
				}
				default: {
					Debug.LogErrorFormat("Hand.Update: unsupported state '{0}'", _curState.ToString());
					return;
				}
			}
		}

		bool TryMoveToPoint(Vector3 endPoint) {
			var handPos  = transform.position;
			var distance = Vector2.Distance(endPoint, handPos);
			if ( distance <= _movementSpeed * Time.deltaTime ) {
				transform.position = endPoint;
				return true;
			}
			transform.Translate((endPoint - handPos).normalized * (_movementSpeed * Time.deltaTime));
			return false;
		}

		public bool TryStartGrab(bool isSecondGrab) {
			if ( _curState != State.Idle ) {
				return false;
			}
			_curState  = State.Chasing;
			_targetPos = Soap.transform.position;
			_movementSpeed =
				isSecondGrab
					? SecondGrabMovementSpeed
					: Mathf.Lerp(MinMovementSpeed, MaxMovementSpeed, LevelManager.LevelProgress);
			return true;
		}

		public bool TryReleaseSoap() {
			if ( _curState != State.Grabbed ) {
				return false;
			}
			_curState = State.Returning;
			return true;
		}

		public void TryForceReturn() {
			if ( IsIdle || IsReturning ) {
				return;
			}
			_curState = State.Returning;
		}

		void OnNotifierTriggerExit(GameObject obj) {
			if ( obj.GetComponent<Soap>() ) {
				Assert.IsTrue(_isOverSoap);
				_isOverSoap = false;
			}
		}

		void OnNotifierTriggerEnter(GameObject obj) {
			if ( obj.GetComponent<Soap>() ) {
				Assert.IsFalse(_isOverSoap);
				_isOverSoap = true;
			}
		}
	}
}

using UnityEngine;
using UnityEngine.Assertions;

using System;

namespace SGJ.Core.Bathroom {
	public sealed class HandsManager : MonoBehaviour {
		enum State {
			Delay,
			HandActive,
			SecondGrab,
			Idle
		}

		[Header("Parameters")]
		public float MinHandsDelay;
		public float MaxHandsDelay;
		[Header("Dependencies")]
		public BathroomLevelManager LevelManager;
		public Soap Soap;
		public Hand LeftHand;
		public Hand RightHand;

		State _curState = State.Idle;

		float _curDelay;
		float _delayTimer;

		SideType _lastSideType = SideType.Unknown;

		public event Action OnSoapFullyGrabbed;

		void Update() {
			switch ( _curState ) {
				case State.Delay: {
					_delayTimer += Time.deltaTime;
					if ( _delayTimer >= _curDelay ) {
						if ( TryActivateHand(false) ) {
							_delayTimer = 0f;
							_curState = State.HandActive;
						}
					}
					break;
				}
				case State.HandActive: {
					break;
				}
				case State.SecondGrab: {
					if ( LeftHand.IsIdle ) {
						TryActivateHand(SideType.Left, true);
					}
					if ( !RightHand.IsIdle ) {
						TryActivateHand(SideType.Right, true);
					}
					break;
				}
				case State.Idle: {
					break;
				}
				default: {
					Debug.LogErrorFormat("HandsManager.Update: unsupported state '{0}'", _curState.ToString());
					return;
				}
			}
		}

		public void Activate() {
			_curState = State.Delay;
			ResetDelay();

			Soap.OnMoved += OnSoapMoved;
		}

		public void Deactivate(bool returnHands) {
			if ( returnHands ) {
				LeftHand.TryForceReturn();
				RightHand.TryForceReturn();
			}
			UnsubscribeFromHandEvents(LeftHand);
			UnsubscribeFromHandEvents(RightHand);
			_curState = State.Idle;
		}

		bool TryActivateHand(bool isSecondGrab) {
			return TryActivateHand((_lastSideType == SideType.Right) ? SideType.Left : SideType.Right, isSecondGrab);
		}

		bool TryActivateHand(SideType sideType, bool isSecondGrab) {
			var hand = (sideType == SideType.Right) ? RightHand : LeftHand;
			if ( hand.TryStartGrab(isSecondGrab) ) {
				hand.OnGrabbedSoap += OnHandGrabbedSoap;
				hand.OnMissedSoap  += OnHandMissedSoap;
				return true;
			}
			return false;
		}

		void UnsubscribeFromHandEvents(Hand hand) {
			Assert.IsTrue(hand);
			hand.OnGrabbedSoap -= OnHandGrabbedSoap;
			hand.OnMissedSoap  -= OnHandMissedSoap;
		}

		void ResetDelay() {
			_delayTimer = 0f;
			_curDelay   = Mathf.Lerp(MaxHandsDelay, MinHandsDelay, LevelManager.LevelProgress);
		}

		void OnSoapMoved(SideType sideType) {
			var hand = (sideType == SideType.Left) ? LeftHand : RightHand;
			if ( hand.TryReleaseSoap() ) {
				if ( _curState == State.SecondGrab ) {
					var otherHand = (hand == LeftHand) ? RightHand : LeftHand;
					if ( otherHand.IsChasing ) {
						_curState = State.HandActive;
					} else {
						_curState = State.Delay;
						ResetDelay();
					}
				}
			}
		}

		void OnHandGrabbedSoap(Hand hand) {
			UnsubscribeFromHandEvents(hand);
			_lastSideType = GetHandSideType(hand);
			switch ( _curState ) {
				case State.SecondGrab: {
					if ( LeftHand.IsGrabbing && RightHand.IsGrabbing ) {
						_curState = State.Idle;
						OnSoapFullyGrabbed?.Invoke();
					} else {
						Assert.IsTrue(LeftHand.IsGrabbing || RightHand.IsGrabbing);
						TryActivateHand(true);
						_curState = State.SecondGrab;
					}
					break;
				}
				case State.HandActive: {
					TryActivateHand(true);
					_curState = State.SecondGrab;
					break;
				}
				default: {
					Debug.LogErrorFormat("HandsManager.OnHandGrabbedSoap: unsupported scenario, current state is '{0}'",
						_curState.ToString());
					break;
				}
			}
		}

		void OnHandMissedSoap(Hand hand) {
			UnsubscribeFromHandEvents(hand);
			_lastSideType = GetHandSideType(hand);
			_curState     = (!RightHand.IsGrabbing && !LeftHand.IsGrabbing) ? State.Delay : State.SecondGrab;
			if ( _curState == State.Delay ) {
				ResetDelay();
			}
		}

		SideType GetHandSideType(Hand hand) {
			if ( hand == LeftHand ) {
				return SideType.Left;
			}
			if ( hand == RightHand ) {
				return SideType.Right;
			}
			Debug.LogErrorFormat("HandsManager.GetSideType: unrecognized hand");
			return SideType.Unknown;
		}
	}
}

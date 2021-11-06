using UnityEngine;

using System;

using SGJ.Utils;

using Random = UnityEngine.Random;

namespace SGJ.Core.Bathroom {
	public sealed class Soap : MonoBehaviour {
		[Header("Parameters")]
		public float MinMovementForce;
		public float MaxMovementForce;
		public float ReloadTime;
		[Header("Dependencies")]
		public ColliderNotifier2D LeftNotifier;
		public ColliderNotifier2D RightNotifier;

		Rigidbody2D _rigidbody;

		bool  _isReloading;
		float _reloadTimer;

		bool _isActive;

		public event Action<SideType> OnMoved;

		void Start() {
			_rigidbody            =  GetComponent<Rigidbody2D>();
			LeftNotifier.OnClick  += OnLeftClick;
			RightNotifier.OnClick += OnRightClick;
		}

		void Update() {
			if ( _isReloading ) {
				_reloadTimer += Time.deltaTime;
				if ( _reloadTimer >= ReloadTime ) {
					_reloadTimer = 0f;
					_isReloading = false;
				}
			}
		}

		public void Activate() {
			_isActive = true;
		}

		public void Deactivate() {
			_isActive = false;
		}

		void OnLeftClick() {
			if ( !_isActive ) {
				return;
			}
			TryPush(SideType.Left);
		}

		void OnRightClick() {
			if ( !_isActive ) {
				return;
			}
			TryPush(SideType.Right);
		}

		void TryPush(SideType sideType) {
			if ( _isReloading ) {
				return;
			}
			var direction = (sideType == SideType.Left) ? Vector2.right : Vector2.left;
			_rigidbody.AddForce(direction.normalized * Random.Range(MinMovementForce, MaxMovementForce),
				ForceMode2D.Impulse);
			OnMoved?.Invoke(sideType);
			_isReloading = true;
		}
	}
}

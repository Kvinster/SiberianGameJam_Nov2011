using UnityEngine;

using System;

namespace SGJ.Core.Kitchen {
	public sealed class KitchenUi : MonoBehaviour {
		[Header("Parameters")]
		public float ShowTime;
		[Header("Dependencies")]
		public GameObject WinScreen;
		public GameObject LoseScreen;

		bool   _isActive;
		float  _timer;
		Action _endAction;

		void Start() {
			WinScreen.SetActive(false);
			LoseScreen.SetActive(false);
		}

		void Update() {
			if ( _isActive ) {
				_timer += Time.deltaTime;
				if ( _timer >= ShowTime ) {
					_endAction?.Invoke();
					_isActive = false;
				}
			}
		}

		public void Win(Action endAction) {
			WinScreen.SetActive(true);
			_isActive  = true;
			_endAction = endAction;
		}

		public void Lose(Action endAction) {
			LoseScreen.SetActive(true);
			_isActive  = true;
			_endAction = endAction;
		}
	}
}

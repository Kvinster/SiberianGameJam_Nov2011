using UnityEngine;

using SGJ.Service;

namespace SGJ.Core.Bathroom {
	public sealed class SoapSoundPlayer : MonoBehaviour {
		public Soap      Soap;
		public AudioClip Clip;

		void OnEnable() {
			Soap.OnMoved += OnSoapMoved;
		}

		void OnDisable() {
			if ( Soap ) {
				Soap.OnMoved -= OnSoapMoved;
			}
		}

		void OnSoapMoved(SideType obj) {
			AudioService.PlaySound(Clip);
		}
	}
}

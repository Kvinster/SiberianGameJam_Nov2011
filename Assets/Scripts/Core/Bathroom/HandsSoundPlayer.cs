using UnityEngine;

using SGJ.Service;

namespace SGJ.Core.Bathroom {
	public sealed class HandsSoundPlayer : MonoBehaviour {
		public Hand[]    Hands;
		public AudioClip GrabbedClip;
		public AudioClip MissedClip;

		void OnEnable() {
			foreach ( var hand in Hands ) {
				hand.OnGrabbedSoap += OnHandGrabbedSoap;
				hand.OnMissedSoap += OnHandMissedSoap;
			}
		}

		void OnDisable() {
			AudioService.StopInPool(this);
		}

		void OnHandMissedSoap(Hand obj) {
			AudioService.StopInPool(this);
			AudioService.PlayInPool(this, MissedClip, false);
		}

		void OnHandGrabbedSoap(Hand obj) {
			AudioService.StopInPool(this);
			AudioService.PlayInPool(this, GrabbedClip, false);
		}
	}
}

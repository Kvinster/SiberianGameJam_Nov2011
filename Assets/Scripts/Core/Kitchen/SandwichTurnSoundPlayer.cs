using UnityEngine;

using SGJ.Service;

namespace SGJ.Core.Kitchen {
	public sealed class SandwichTurnSoundPlayer : MonoBehaviour {
		public Sandwich  Sandwich;
		public AudioClip Clip;

		void OnEnable() {
			Sandwich.OnTurned += OnSandwichTurned;
		}

		void OnDisable() {
			if ( Sandwich ) {
				Sandwich.OnTurned -= OnSandwichTurned;
			}
		}

		void OnSandwichTurned() {
			AudioService.PlaySound(Clip);
		}
	}
}

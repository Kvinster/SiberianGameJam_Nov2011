using UnityEngine;

using SGJ.Service;

namespace SGJ.Core.Toilet {
	public sealed class ToiletPartSoundPlayer : MonoBehaviour {
		public BaseToiletPart ToiletPart;
		public AudioClip      Clip;

		void OnEnable() {
			ToiletPart.OnClicksChanged += OnToiletPartClicksChanged;
		}

		void OnDisable() {
			if ( ToiletPart ) {
				ToiletPart.OnClicksChanged -= OnToiletPartClicksChanged;
			}
		}

		void OnToiletPartClicksChanged() {
			AudioService.PlaySound(Clip);
		}
	}
}

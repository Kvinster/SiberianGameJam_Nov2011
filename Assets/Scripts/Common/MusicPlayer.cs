using UnityEngine;

using SGJ.Service;

namespace SGJ.Common {
	public sealed class MusicPlayer : MonoBehaviour {
		public AudioClip Clip;

		void OnEnable() {
			AudioService.PlayInPool(this, Clip, true);
		}

		void OnDisable() {
			Stop();
		}

		public void Stop() {
			AudioService.StopInPool(this);
		}
	}
}

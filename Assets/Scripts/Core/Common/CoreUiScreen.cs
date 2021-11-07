using UnityEngine;

using SGJ.Common;
using SGJ.Service;

using DG.Tweening;

namespace SGJ.Core.Common {
	public sealed class CoreUiScreen : MonoBehaviour {
		public CanvasGroup CanvasGroup;
		public AudioClip   Clip;
		public MusicPlayer MusicPlayer;

		void Reset() {
			CanvasGroup = GetComponent<CanvasGroup>();
		}

		void OnDisable() {
			if ( Clip ) {
				AudioService.StopInPool(this);
			}
		}

		public void InitHidden() {
			CanvasGroup.alpha = 0f;
			gameObject.SetActive(false);
		}

		public void Show(float showTime) {
			CanvasGroup.alpha = 0f;
			gameObject.SetActive(true);
			CanvasGroup.DOFade(1f, showTime);

			if ( Clip ) {
				AudioService.PlayInPool(this, Clip, true);
			}
			if ( MusicPlayer ) {
				MusicPlayer.Stop();
			}
		}
	}
}

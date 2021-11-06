using UnityEngine;

using DG.Tweening;

namespace SGJ.Core.Common {
	public sealed class CoreUiScreen : MonoBehaviour {
		public CanvasGroup CanvasGroup;

		void Reset() {
			CanvasGroup = GetComponent<CanvasGroup>();
		}

		public void InitHidden() {
			CanvasGroup.alpha = 0f;
			gameObject.SetActive(false);
		}

		public void Show(float showTime) {
			CanvasGroup.alpha = 0f;
			gameObject.SetActive(true);
			CanvasGroup.DOFade(1f, showTime);
		}
	}
}

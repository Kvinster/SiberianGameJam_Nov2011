using UnityEngine;

using TMPro;

namespace SGJ.Core.Toilet {
	public sealed class DummyToiletPart : BaseToiletPart {
		[Header("Impl Dependencies")]
		public TMP_Text Text;

		protected override void OnCurClicksChanged() {
			Text.text = $"{CurClicks} / {TotalClicks}";
		}

		protected override void OnFinishStarted() {
			FinishFinish();
		}
	}
}

using UnityEngine;

namespace SGJ.Core.Toilet {
	public sealed class DummyToiletPart : BaseToiletPart {
		public SpriteRenderer SpriteRenderer;
		public Sprite[]       StateSprites;
		public Sprite         FinalSprite;

		protected override void OnCurClicksChanged() {
			if ( CurClicks == 0 ) {
				SpriteRenderer.sprite = null;
				return;
			}
			if ( CurClicks == TotalClicks ) {
				SpriteRenderer.sprite = FinalSprite;
				return;
			}
			var index = Mathf.Clamp(Mathf.FloorToInt((float) StateSprites.Length * CurClicks / TotalClicks), 0, StateSprites.Length - 1);
			SpriteRenderer.sprite = StateSprites[index];
		}

		protected override void OnFinishStarted() {
			FinishFinish();
		}
	}
}

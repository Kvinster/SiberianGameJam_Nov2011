using UnityEngine;

using SGJ.Utils;

using DG.Tweening;

namespace SGJ.Core.Toilet {
	public sealed class ToiletCutscenePlayer : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 1f;
		public float FadeTime  = 0.5f;
		public float PauseTime = 0.5f;
		[Header("Dependencies")]
		public ToiletLevelManager LevelManager;
		[Space]
		public Transform HandTransform;
		public Transform HandStartPos;
		public Transform HandEndPos;
		[Space]
		public AlphaGroup Cutscene0AlphaGroup;
		public AlphaGroup Cutscene1AlphaGroup;
		public AlphaGroup GameplayAlphaGroup;

		void Start() {
			Play();
		}

		void Play() {
			HandTransform.position    = HandStartPos.position;
			HandTransform.rotation    = HandStartPos.rotation;
			Cutscene0AlphaGroup.Alpha = 1f;
			Cutscene1AlphaGroup.Alpha = 0f;
			GameplayAlphaGroup.Alpha  = 0f;

			DOTween.Sequence()
				.AppendInterval(PlayTime)
				.Append(DOTween.To(() => Cutscene0AlphaGroup.Alpha, x => Cutscene0AlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => Cutscene1AlphaGroup.Alpha, x => Cutscene1AlphaGroup.Alpha   = x, 1f, FadeTime))
				.Append(HandTransform.DOMove(HandEndPos.position, PlayTime))
				.Join(HandTransform.DORotate(HandEndPos.rotation.eulerAngles, PlayTime))
				.Append(DOTween.To(() => Cutscene1AlphaGroup.Alpha, x => Cutscene1AlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => GameplayAlphaGroup.Alpha, x => GameplayAlphaGroup.Alpha     = x, 1f, FadeTime))
				.AppendInterval(PauseTime)
				.OnComplete(LevelManager.StartLevel);
		}
	}
}

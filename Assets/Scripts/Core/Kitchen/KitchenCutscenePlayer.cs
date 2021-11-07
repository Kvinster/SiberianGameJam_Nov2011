using UnityEngine;

using SGJ.Utils;

using DG.Tweening;

namespace SGJ.Core.Kitchen {
	public sealed class KitchenCutscenePlayer : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 1f;
		public float FadeTime  = 0.5f;
		public float PauseTime = 0.5f;
		[Header("Dependencies")]
		public KitchenLevelManager LevelManager;
		[Space]
		public Transform LadyTransform;
		public Transform LadyStartPos;
		public Transform LadyEndPos;
		[Space]
		public AlphaGroup Cutscene0AlphaGroup;
		public AlphaGroup Cutscene1AlphaGroup;
		public AlphaGroup GameplayAlphaGroup;

		void Start() {
			Play();
		}

		void Play() {
			LadyTransform.position    = LadyStartPos.position;
			LadyTransform.rotation    = LadyStartPos.rotation;
			Cutscene0AlphaGroup.Alpha = 1f;
			Cutscene1AlphaGroup.Alpha = 0f;
			GameplayAlphaGroup.Alpha  = 0f;

			DOTween.Sequence()
				.AppendInterval(PlayTime)
				.Append(LadyTransform.DOMove(LadyEndPos.position, PlayTime).SetEase(Ease.InSine))
				.Join(LadyTransform.DORotate(LadyEndPos.rotation.eulerAngles, PlayTime))
				.Append(DOTween.To(() => Cutscene0AlphaGroup.Alpha, x => Cutscene0AlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => Cutscene1AlphaGroup.Alpha, x => Cutscene1AlphaGroup.Alpha   = x, 1f, FadeTime))
				.AppendInterval(PlayTime)
				.Append(DOTween.To(() => Cutscene1AlphaGroup.Alpha, x => Cutscene1AlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => GameplayAlphaGroup.Alpha, x => GameplayAlphaGroup.Alpha   = x, 1f, FadeTime))
				.AppendInterval(PauseTime)
				.OnComplete(LevelManager.StartLevel);
		}
	}
}

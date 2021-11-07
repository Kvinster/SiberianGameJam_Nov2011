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
		public AlphaGroup CutsceneAlphaGroup;
		public AlphaGroup GameplayAlphaGroup;

		void Start() {
			Play();
		}

		void Play() {
			LadyTransform.position   = LadyStartPos.position;
			LadyTransform.rotation   = LadyStartPos.rotation;
			CutsceneAlphaGroup.Alpha = 1f;
			GameplayAlphaGroup.Alpha = 0f;

			DOTween.Sequence()
				.AppendInterval(PlayTime)
				.Append(LadyTransform.DOMove(LadyEndPos.position, PlayTime).SetEase(Ease.InSine))
				.Join(LadyTransform.DORotate(LadyEndPos.rotation.eulerAngles, PlayTime))
				.Append(DOTween.To(() => CutsceneAlphaGroup.Alpha, x => CutsceneAlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => GameplayAlphaGroup.Alpha, x => GameplayAlphaGroup.Alpha   = x, 1f, FadeTime))
				.AppendInterval(PauseTime)
				.OnComplete(LevelManager.StartLevel);
		}
	}
}

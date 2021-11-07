using UnityEngine;

using SGJ.Utils;

using DG.Tweening;

namespace SGJ.Core.Bathroom {
	public sealed class BathroomCutscenePlayer : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 1f;
		public float FadeTime  = 0.5f;
		public float PauseTime = 0.5f;
		[Header("Dependencies")]
		public BathroomLevelManager LevelManager;
		[Space]
		public Transform GrandpaTransform;
		public Transform GrandpaStartPos;
		public Transform GrandpaEndPos;
		[Space]
		public AlphaGroup Cutscene0AlphaGroup;
		public AlphaGroup Cutscene1AlphaGroup;
		public AlphaGroup GameplayAlphaGroup;

		void Start() {
			Play();
		}

		void Play() {
			GrandpaTransform.position = GrandpaStartPos.position;
			GrandpaTransform.rotation = GrandpaStartPos.rotation;
			Cutscene0AlphaGroup.Alpha = 1f;
			Cutscene1AlphaGroup.Alpha = 0f;
			GameplayAlphaGroup.Alpha  = 0f;

			DOTween.Sequence()
				.AppendInterval(PlayTime)
				.Append(DOTween.To(() => Cutscene0AlphaGroup.Alpha, x => Cutscene0AlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => Cutscene1AlphaGroup.Alpha, x => Cutscene1AlphaGroup.Alpha = x, 1f, FadeTime))
				.Append(GrandpaTransform.DOMove(GrandpaEndPos.position, PlayTime))
				.Join(GrandpaTransform.DORotate(GrandpaEndPos.rotation.eulerAngles, PlayTime))
				.Append(DOTween.To(() => Cutscene1AlphaGroup.Alpha, x => Cutscene1AlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => GameplayAlphaGroup.Alpha, x => GameplayAlphaGroup.Alpha   = x, 1f, FadeTime))
				.AppendInterval(PauseTime)
				.OnComplete(LevelManager.StartLevel);
		}
	}
}

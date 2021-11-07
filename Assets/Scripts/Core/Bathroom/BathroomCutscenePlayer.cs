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
		public AlphaGroup CutsceneAlphaGroup;
		public AlphaGroup GameplayAlphaGroup;

		Tween _anim;

		void Start() {
			Play();
		}

		void Play() {
			GrandpaTransform.position = GrandpaStartPos.position;
			GrandpaTransform.rotation = GrandpaStartPos.rotation;
			CutsceneAlphaGroup.Alpha  = 1f;
			GameplayAlphaGroup.Alpha  = 0f;

			_anim = DOTween.Sequence()
				.Append(GrandpaTransform.DOMove(GrandpaEndPos.position, PlayTime))
				.Join(GrandpaTransform.DORotate(GrandpaEndPos.rotation.eulerAngles, PlayTime))
				.Append(DOTween.To(() => CutsceneAlphaGroup.Alpha, x => CutsceneAlphaGroup.Alpha = x, 0f, FadeTime))
				.Join(DOTween.To(() => GameplayAlphaGroup.Alpha, x => GameplayAlphaGroup.Alpha   = x, 1f, FadeTime))
				.AppendInterval(PauseTime)
				.OnComplete(LevelManager.StartLevel);
		}
	}
}

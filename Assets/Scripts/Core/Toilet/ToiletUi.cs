using UnityEngine;

using SGJ.Core.Common;

namespace SGJ.Core.Toilet {
	public sealed class ToiletUi : MonoBehaviour {
		[Header("Parameters")]
		public float ShowTime = 3f;
		public float ScreenShowTime = 0.25f;
		[Header("Dependencies")]
		public ToiletLevelManager LevelManager;
		public GameObject   LevelUiRoot;
		public CoreUiScreen WinScreen;
		public CoreUiScreen LoseScreen;

		bool  _isEndScreenActive;
		float _timer;

		void Start() {
			WinScreen.InitHidden();
			LoseScreen.InitHidden();
			LevelUiRoot.SetActive(true);
			LevelManager.OnLevelFinished += OnLevelFinished;
		}

		void Update() {
			if ( _isEndScreenActive ) {
				_timer += Time.deltaTime;
				if ( _timer >= ShowTime ) {
					_isEndScreenActive = false;
					LevelManager.ExitToMeta();
				}
			}
		}

		void OnLevelFinished(bool win) {
			if ( win ) {
				Win();
			} else {
				Lose();
			}
		}

		void Win() {
			WinScreen.Show(ScreenShowTime);
			LevelUiRoot.SetActive(false);
			_isEndScreenActive = true;
		}

		void Lose() {
			LoseScreen.Show(ScreenShowTime);
			LevelUiRoot.SetActive(false);
			_isEndScreenActive = true;
		}
	}
}

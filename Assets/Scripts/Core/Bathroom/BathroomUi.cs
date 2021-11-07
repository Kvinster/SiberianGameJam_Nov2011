using UnityEngine;

using SGJ.Core.Common;

using TMPro;

namespace SGJ.Core.Bathroom {
	public sealed class BathroomUi : MonoBehaviour {
		[Header("Parameters")]
		public float WinShowTime = 3.3f;
		public float LoseShowTime   = 3.9f;
		public float ScreenShowTime = 0.25f;
		[Header("Dependencies")]
		public BathroomLevelManager LevelManager;
		public GameObject   LevelUiRoot;
		public TMP_Text     TimerText;
		public CoreUiScreen WinScreen;
		public CoreUiScreen LoseScreen;

		bool  _isEndScreenActive;
		float _timer;

		float _showTime;

		void Start() {
			WinScreen.InitHidden();
			LoseScreen.InitHidden();
			LevelUiRoot.SetActive(true);
			LevelManager.OnLevelFinished += OnLevelFinished;
		}

		void Update() {
			if ( _isEndScreenActive ) {
				_timer += Time.deltaTime;
				if ( _timer >= _showTime ) {
					_isEndScreenActive = false;
					LevelManager.ExitToMeta();
				}
			} else {
				TimerText.text = string.Format("Time: {0:F1}", LevelManager.TimeLeft);
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
			_showTime = WinShowTime;
			WinScreen.Show(ScreenShowTime);
			LevelUiRoot.SetActive(false);
			_isEndScreenActive = true;
		}

		void Lose() {
			_showTime = LoseShowTime;
			LoseScreen.Show(ScreenShowTime);
			LevelUiRoot.SetActive(false);
			_isEndScreenActive = true;
		}
	}
}

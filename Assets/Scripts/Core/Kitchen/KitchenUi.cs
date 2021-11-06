using UnityEngine;

using TMPro;

namespace SGJ.Core.Kitchen {
	public sealed class KitchenUi : MonoBehaviour {
		[Header("Parameters")]
		public float ShowTime;
		public float ScreenShowTime = 1f;
		[Header("Dependencies")]
		public KitchenLevelManager LevelManager;
		public GameObject LevelUiRoot;
		public TMP_Text   TimerText;
		public TMP_Text   ProgressText;
		public KitchenUiScreen WinScreen;
		public KitchenUiScreen LoseScreen;

		bool  _isEndScreenActive;
		float _timer;

		void Start() {
			WinScreen.InitHidden();
			LoseScreen.InitHidden();
			LevelUiRoot.SetActive(true);
			LevelManager.OnCurProgressChanged += OnCurProgressChanged;
			LevelManager.OnLevelFinished      += OnLevelFinished;
			OnCurProgressChanged(LevelManager.CurProgress);
		}

		void Update() {
			if ( _isEndScreenActive ) {
				_timer += Time.deltaTime;
				if ( _timer >= ShowTime ) {
					_isEndScreenActive = false;
					LevelManager.ExitToMeta();
				}
			} else {
				TimerText.text = string.Format("Time: {0:F1}", LevelManager.TimeLeft);
			}
		}

		void OnCurProgressChanged(int curProgress) {
			ProgressText.text = $"Progress: {curProgress} / {LevelManager.Goal}";
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

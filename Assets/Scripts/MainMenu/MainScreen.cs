using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

namespace SGJ.MainScreen {
	public sealed class MainScreen : MonoBehaviour {
		public Button   StartGameButton;
		public Button   ExitButton;
		public TMP_Text VersionText;

		void Start() {
			StartGameButton.onClick.AddListener(OnStartGameClick);
			ExitButton.onClick.AddListener(OnExitClick);
			VersionText.text = $"Version: {Application.version}";
		}

		void OnStartGameClick() {
			SceneManager.LoadScene("Meta");
		}

		void OnExitClick() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
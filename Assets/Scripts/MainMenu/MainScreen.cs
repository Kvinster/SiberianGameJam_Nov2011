using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

namespace SGJ.MainScreen {
	public sealed class MainScreen : MonoBehaviour {
		public Button   StartGameButton;
		public TMP_Text VersionText;

		void Start() {
			StartGameButton.onClick.AddListener(OnStartGameClick);
			VersionText.text = $"Version: {Application.version}";
		}

		void OnStartGameClick() {
			SceneManager.LoadScene("Meta");
		}
	}
}
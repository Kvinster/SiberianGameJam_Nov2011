using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SGJ.MainScreen {
	public sealed class MainScreen : MonoBehaviour {
		public Button StartGameButton;

		void Start() {
			StartGameButton.onClick.AddListener(OnStartGameClick);
		}

		void OnStartGameClick() {
			SceneManager.LoadScene("Meta");
		}
	}
}
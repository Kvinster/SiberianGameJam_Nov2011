using UnityEngine;
using UnityEngine.UI;

namespace SGJ.MainScreen {
	public sealed class MainScreen : MonoBehaviour {
		public Button StartGameButton;

		void Start() {
			StartGameButton.onClick.AddListener(OnStartGameClick);
		}

		void OnStartGameClick() {
			// TODO: change scene, remove debug log
			Debug.Log("Start game");
		}
	}
}
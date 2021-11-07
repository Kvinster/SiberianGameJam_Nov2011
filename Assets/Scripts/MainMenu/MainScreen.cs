using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using SGJ.Common;
using SGJ.Controllers;

using TMPro;

namespace SGJ.MainScreen {
	public sealed class MainScreen : MonoBehaviour {
		public Button   StartGameButton;
		public Button   ExitButton;
		public TMP_Text VersionText;

		bool _showCheats;

		void Start() {
			StartGameButton.onClick.AddListener(OnStartGameClick);
			ExitButton.onClick.AddListener(OnExitClick);
			VersionText.text = $"Version: {Application.version}";
		}

		void Update() {
			if ( Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2) &&
			     Input.GetKey(KeyCode.Alpha3) ) {
				_showCheats = true;
			}
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

		void OnGUI() {
			if ( _showCheats ) {
				GUILayout.Space(10);
				for ( var i = 0; i < LevelController.RoomsOrder.Length; i++ ) {
					var roomType = LevelController.RoomsOrder[i];
					if ( GUILayout.Button(roomType.ToString(),
						new GUIStyle(GUI.skin.button) { fontSize = 20 * Mathf.RoundToInt(Screen.height / 1080f) },
						GUILayout.MinWidth(Screen.width / 16f), GUILayout.MinHeight(Screen.height / 18f)) ) {
						LevelController.Instance.CheatSetNextLevelIndex(i);
						SceneManager.LoadScene(RoomTypeHelper.RoomTypeToSceneName(roomType));
						return;
					}
				}
			}
		}
	}
}

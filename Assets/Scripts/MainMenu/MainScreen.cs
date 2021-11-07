using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using SGJ.Common;
using SGJ.Controllers;

using DG.Tweening;
using TMPro;

namespace SGJ.MainScreen {
	public sealed class MainScreen : MonoBehaviour {
		[Header("Parameters")]
		public float IntroFadeTime = 0.5f;
		[Header("Dependencies")]
		public Button   StartGameButton;
		public Button     ExitButton;
		public TMP_Text   VersionText;
		public GameObject IntroRoot;
		public Graphic    IntroGraphic;

		bool _showCheats;
		bool _isIntroShown;

		void Start() {
			StartGameButton.onClick.AddListener(OnStartGameClick);
			ExitButton.onClick.AddListener(OnExitClick);
			IntroRoot.SetActive(false);
			VersionText.text = $"Version: {Application.version}";
		}

		void Update() {
			if ( Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2) &&
			     Input.GetKey(KeyCode.Alpha3) ) {
				_showCheats = true;
			}
			if ( _isIntroShown && Input.anyKey ) {
				StartGame();
			}
		}

		void OnStartGameClick() {
			IntroRoot.SetActive(true);
			IntroGraphic.color = new Color(1f, 1f, 1f, 0f);
			IntroGraphic.DOFade(1f, IntroFadeTime);
			_isIntroShown = true;
		}

		void OnExitClick() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		void StartGame() {
			LevelController.Instance.ResetProgress();
			SceneManager.LoadScene("Meta");
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

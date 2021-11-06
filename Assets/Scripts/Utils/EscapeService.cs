using UnityEngine;
using UnityEngine.SceneManagement;

using SGJ.Controllers;

namespace SGJ.Utils {
	public sealed class EscapeService : MonoBehaviour {
		static EscapeService _instance;

		void OnEnable() {
			if ( !_instance ) {
				_instance = this;
			} else {
				Destroy(gameObject);
			}
		}

		void Start() {
			DontDestroyOnLoad(gameObject);
		}

		void Update() {
			if ( Input.GetKeyDown(KeyCode.Escape) ) {
				if ( SceneManager.GetActiveScene().name == "MainMenu" ) {
					LevelController.Instance.ResetProgress();
#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
#else
					Application.Quit();
#endif
				} else {
					SceneManager.LoadScene("MainMenu");
				}
			}
		}
	}
}

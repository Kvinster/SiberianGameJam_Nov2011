using UnityEngine;
using UnityEngine.SceneManagement;

using System;

using SGJ.Controllers;

namespace SGJ.Core.Toilet {
	public sealed class ToiletLevelManager : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 30f;
		[Header("Dependencies")]
		public GameObject TutorialRoot;

		float _playTimer;

		bool _isTutorialShown;

		public float LevelProgress => Mathf.Clamp01(_playTimer / PlayTime);

		bool IsLevelActive { get; set; }

		public event Action<bool> OnLevelFinished;

		void Start() {
			BaseToiletPart.OnToiletPartDestroyed += OnToiletPartDestroyed;
		}

		void OnDisable() {
			BaseToiletPart.OnToiletPartDestroyed -= OnToiletPartDestroyed;
		}

		void Update() {
			if ( _isTutorialShown && Input.anyKey ) {
				TutorialRoot.SetActive(false);
				_isTutorialShown = false;
				Activate();
			}
			if ( !IsLevelActive ) {
				return;
			}
			_playTimer += Time.deltaTime;
			if ( _playTimer >= PlayTime ) {
				FinishLevel(false);
				IsLevelActive = false;
			}
		}

		public void StartLevel() {
			_isTutorialShown = true;
			TutorialRoot.SetActive(true);
		}

		public void ExitToMeta() {
			SceneManager.LoadScene("Meta");
		}

		void Activate() {
			IsLevelActive = true;
			foreach ( var toiletPart in BaseToiletPart.Instances ) {
				toiletPart.IsActive = true;
			}
		}

		void OnToiletPartDestroyed() {
			if ( !IsLevelActive ) {
				return;
			}
			if ( BaseToiletPart.Instances.Count == 0 ) {
				FinishLevel(true);
			}
		}

		void FinishLevel(bool win) {
			IsLevelActive = false;
			if ( win ) {
				LevelController.Instance.TryAdvance();
			} else {
				LevelController.Instance.ResetProgress();
			}
			OnLevelFinished?.Invoke(win);
		}
	}
}

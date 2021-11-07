using UnityEngine;
using UnityEngine.SceneManagement;

using System;

using SGJ.Controllers;

namespace SGJ.Core.Toilet {
	public sealed class ToiletLevelManager : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 30f;

		float _playTimer;

		public float LevelProgress => Mathf.Clamp01(_playTimer / PlayTime);

		bool IsLevelActive { get; set; }

		public event Action<bool> OnLevelFinished;

		void Start() {
			IsLevelActive = true;
			BaseToiletPart.OnToiletPartDestroyed += OnToiletPartDestroyed;
		}

		void OnDisable() {
			BaseToiletPart.OnToiletPartDestroyed -= OnToiletPartDestroyed;
		}

		void OnToiletPartDestroyed() {
			if ( !IsLevelActive ) {
				return;
			}
			if ( BaseToiletPart.Instances.Count == 0 ) {
				FinishLevel(true);
			}
		}

		void Update() {
			if ( !IsLevelActive ) {
				return;
			}
			_playTimer += Time.deltaTime;
			if ( _playTimer >= PlayTime ) {
				FinishLevel(false);
				IsLevelActive = false;
			}
		}

		public void ExitToMeta() {
			SceneManager.LoadScene("Meta");
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

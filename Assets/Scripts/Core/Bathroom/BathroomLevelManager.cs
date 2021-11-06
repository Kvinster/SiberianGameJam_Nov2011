using UnityEngine;
using UnityEngine.SceneManagement;

using System;

using SGJ.Controllers;

namespace SGJ.Core.Bathroom {
	public sealed class BathroomLevelManager : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 30f;
		[Header("Dependencies")]
		public Soap Soap;
		public HandsManager HandsManager;

		float _playTimer;

		public float TimeLeft      => Mathf.Max(0f, PlayTime - _playTimer);
		public float LevelProgress => Mathf.Clamp01(_playTimer / PlayTime);

		bool IsLevelActive { get; set; }

		public event Action<bool> OnLevelFinished;

		void Start() {
			IsLevelActive = true;
			Soap.Activate();
			HandsManager.Activate();
			HandsManager.OnSoapFullyGrabbed += OnSoapFullyGrabbed;
		}

		void Update() {
			if ( !IsLevelActive ) {
				return;
			}
			_playTimer += Time.deltaTime;
			if ( _playTimer >= PlayTime ) {
				FinishLevel(true);
			}
		}

		public void ExitToMeta() {
			SceneManager.LoadScene("Meta");
		}

		void FinishLevel(bool win) {
			IsLevelActive = false;
			Soap.Deactivate();
			if ( win ) {
				LevelController.Instance.TryAdvance();
			} else {
				LevelController.Instance.ResetProgress();
			}
			HandsManager.Deactivate(win);
			OnLevelFinished?.Invoke(win);
		}

		void OnSoapFullyGrabbed() {
			if ( !IsLevelActive ) {
				return;
			}
			FinishLevel(false);
		}
	}
}

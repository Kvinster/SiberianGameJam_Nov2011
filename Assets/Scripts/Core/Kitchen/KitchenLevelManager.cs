using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using SGJ.Controllers;

namespace SGJ.Core.Kitchen {
	public sealed class KitchenLevelManager : MonoBehaviour {
		[Header("Parameters")]
		public float PlayTime = 30f;
		public int Goal = 15;
		[Header("Dependencies")]
		public GameObject TutorialRoot;
		public SandwichSpawner    SandwichSpawner;
		public KitchenDeathZone   DeathZone;
		public Physics2DRaycaster Raycaster;

		float _timer;
		int   _curProgress;

		bool _isTutorialShown;

		public int CurProgress {
			get => _curProgress;
			private set {
				if ( _curProgress == value ) {
					return;
				}
				_curProgress = value;
				OnCurProgressChanged?.Invoke(_curProgress);
			}
		}

		public float TimeLeft => Mathf.Max(0f, PlayTime - _timer);

		bool IsActive { get; set; }

		public event Action<int>  OnCurProgressChanged;
		public event Action<bool> OnLevelFinished;

		void OnDestroy() {
			UnsubscribeFromDeathZone();
		}

		void Update() {
			if ( _isTutorialShown && Input.anyKey ) {
				TutorialRoot.SetActive(false);
				_isTutorialShown = false;
				Activate();
			}
			if ( !IsActive ) {
				return;
			}
			_timer += Time.deltaTime;
			if ( _timer >= PlayTime ) {
				IsActive = false;
				if ( CurProgress >= Goal ) {
					Win();
				} else {
					Lose();
				}
			}
		}

		public void StartLevel() {
			TutorialRoot.SetActive(true);
			_isTutorialShown = true;
		}

		public void ExitToMeta() {
			SceneManager.LoadScene("Meta");
		}

		void Activate() {
			SandwichSpawner.IsActive  =  true;
			DeathZone.OnSandwichEnter += OnSandwichEnterDeathZone;

			IsActive = true;
		}

		void UnsubscribeFromDeathZone() {
			if ( DeathZone ) {
				DeathZone.OnSandwichEnter -= OnSandwichEnterDeathZone;
			}
		}

		void OnSandwichEnterDeathZone(bool isSandwichTurned) {
			if ( isSandwichTurned && IsActive ) {
				++CurProgress;
			}
		}

		void CommonFinish() {
			Raycaster.enabled        = false;
			SandwichSpawner.IsActive = false;
			UnsubscribeFromDeathZone();

			var sandwiches = new List<Sandwich>(Sandwich.Instances);
			foreach ( var sandwich in sandwiches ) {
				Destroy(sandwich.gameObject);
			}
		}

		void Win() {
			CommonFinish();
			LevelController.Instance.TryAdvance();
			OnLevelFinished?.Invoke(true);
		}

		void Lose() {
			CommonFinish();
			LevelController.Instance.ResetProgress();
			OnLevelFinished?.Invoke(false);
		}
	}
}

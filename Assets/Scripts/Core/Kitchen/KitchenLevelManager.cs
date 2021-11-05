using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using SGJ.Controllers;

namespace SGJ.Core.Kitchen {
	public sealed class KitchenLevelManager : MonoBehaviour {
		public SandwichSpawner    SandwichSpawner;
		public KitchenDeathZone   DeathZone;
		public KitchenUi          Ui;
		public Physics2DRaycaster Raycaster;

		void Start() {
			SandwichSpawner.IsActive           =  true;
			DeathZone.OnAllSandwichesProcessed += OnOnAllSandwichesProcessed;
			DeathZone.OnSandwichEnter          += OnSandwichEnterDeathZone;
		}

		void OnDestroy() {
			UnsubscribeFromDeathZone();
		}

		void UnsubscribeFromDeathZone() {
			if ( DeathZone ) {
				DeathZone.OnAllSandwichesProcessed -= OnOnAllSandwichesProcessed;
				DeathZone.OnSandwichEnter          -= OnSandwichEnterDeathZone;
			}
		}

		void OnSandwichEnterDeathZone() {
			Raycaster.enabled        = false;
			SandwichSpawner.IsActive = false;
			UnsubscribeFromDeathZone();
			LevelController.Instance.ResetProgress();
			Ui.Lose(ExitToMeta);
		}

		void OnOnAllSandwichesProcessed() {
			Raycaster.enabled        = false;
			SandwichSpawner.IsActive = false;
			UnsubscribeFromDeathZone();
			LevelController.Instance.TryAdvance();
			Ui.Win(ExitToMeta);
		}

		void ExitToMeta() {
			SceneManager.LoadScene("Meta");
		}
	}
}

using UnityEngine;

using System;

namespace SGJ.Core.Kitchen {
	[RequireComponent(typeof(Collider2D))]
	public sealed class KitchenDeathZone : MonoBehaviour {
		public SandwichSpawner Spawner;

		int _totalSandwichesProcessed;

		bool _unturnedPassed;

		public event Action OnAllSandwichesProcessed;
		public event Action OnSandwichEnter;

		void OnTriggerEnter2D(Collider2D other) {
			if ( other.TryGetComponent<Sandwich>(out var sandwich) ) {
				++_totalSandwichesProcessed;
				if ( !sandwich.IsTurned ) {
					OnSandwichEnter?.Invoke();
					_unturnedPassed = true;
				}
				if ( !_unturnedPassed && (_totalSandwichesProcessed == Spawner.TotalSandwiches) ) {
					OnAllSandwichesProcessed?.Invoke();
				}
			}
		}
	}
}

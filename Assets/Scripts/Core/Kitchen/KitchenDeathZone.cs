using UnityEngine;

using System;

namespace SGJ.Core.Kitchen {
	[RequireComponent(typeof(Collider2D))]
	public sealed class KitchenDeathZone : MonoBehaviour {
		public event Action<bool> OnSandwichEnter;

		void OnTriggerEnter2D(Collider2D other) {
			if ( other.TryGetComponent<Sandwich>(out var sandwich) ) {
				OnSandwichEnter?.Invoke(sandwich.IsTurned);
			}
		}
	}
}

﻿using UnityEngine;
using UnityEngine.EventSystems;

using System;

namespace SGJ.Utils {
	[RequireComponent(typeof(Collider2D))]
	public sealed class ColliderNotifier2D : MonoBehaviour, IPointerClickHandler {
		public event Action             OnClick;
		public event Action<GameObject> OnTriggerEnter;
		public event Action<GameObject> OnTriggerStay;
		public event Action<GameObject> OnTriggerExit;

		void OnTriggerEnter2D(Collider2D other) {
			OnTriggerEnter?.Invoke(other.gameObject);
		}

		void OnTriggerStay2D(Collider2D other) {
			OnTriggerStay?.Invoke(other.gameObject);
		}

		void OnTriggerExit2D(Collider2D other) {
			OnTriggerExit?.Invoke(other.gameObject);
		}

		public void OnPointerClick(PointerEventData eventData) {
			if ( eventData.button == PointerEventData.InputButton.Left ) {
				OnClick?.Invoke();
			}
		}
	}
}

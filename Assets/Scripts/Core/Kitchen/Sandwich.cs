﻿using UnityEngine;
using UnityEngine.EventSystems;

using System;
using System.Collections.Generic;

using DG.Tweening;

using Random = UnityEngine.Random;

namespace SGJ.Core.Kitchen {
	public sealed class Sandwich : MonoBehaviour, IPointerDownHandler, IPointerClickHandler {
		public static HashSet<Sandwich> Instances = new HashSet<Sandwich>();

		[Header("Parameters")]
		public float TurnTime   = 0.5f;
		public float TurnSpread = 10f;
		[Header("Dependencies")]
		public Collider2D Collider;

		public bool IsTurned { get; private set; }

		Rigidbody2D _rigidbody;

		Tween _anim;

		public event Action OnTurned;

		void OnDestroy() {
			_anim?.Kill();
		}

		void OnEnable() {
			Instances.Add(this);
		}

		void OnDisable() {
			Instances.Remove(this);
		}

		void Start() {
			_rigidbody          = GetComponent<Rigidbody2D>();
			_rigidbody.rotation = 0f;
		}

		public void OnPointerDown(PointerEventData eventData) {
			if ( eventData.button == PointerEventData.InputButton.Left ) {
				TryTurn();
			}
		}

		public void OnPointerClick(PointerEventData eventData) {
			if ( eventData.button == PointerEventData.InputButton.Left ) {
				TryTurn();
			}
		}

		void TryTurn() {
			if ( IsTurned ) {
				return;
			}
			IsTurned = true;

			_anim = _rigidbody.DORotate(
				180 * ((Random.Range(0, 2) == 0) ? 1 : -1) + Random.Range(-TurnSpread, TurnSpread), TurnTime);
			OnTurned?.Invoke();
		}
	}
}

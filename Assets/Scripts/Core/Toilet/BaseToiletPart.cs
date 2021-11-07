using UnityEngine;
using UnityEngine.EventSystems;

using System;
using System.Collections.Generic;

namespace SGJ.Core.Toilet {
	public abstract class BaseToiletPart : MonoBehaviour, IPointerClickHandler {
		public static readonly HashSet<BaseToiletPart> Instances = new HashSet<BaseToiletPart>();

		public static event Action OnToiletPartDestroyed;

		[Header("Parameters")]
		public int TotalClicks;

		Collider2D _collider;

		public bool IsActive { get; set; }

		protected int  CurClicks { get; set; }
		protected bool IsAlive   { get; set; } = true;

		public event Action OnClicksChanged;

		protected virtual void OnEnable() {
			Instances.Add(this);
		}

		protected virtual void OnDisable() {
			Instances.Remove(this);
			OnToiletPartDestroyed?.Invoke();
		}

		protected virtual void Start() {
			_collider = GetComponent<Collider2D>();
			OnCurClicksChanged();
		}

		public void OnPointerClick(PointerEventData eventData) {
			if ( !IsActive || !IsAlive ) {
				return;
			}
			++CurClicks;
			OnCurClicksChanged();
			OnClicksChanged?.Invoke();
			if ( CurClicks >= TotalClicks ) {
				StartFinish();
			}
		}

		protected void FinishFinish() {
			Destroy(_collider);
			enabled = false;
		}

		protected abstract void OnFinishStarted();

		protected virtual void OnCurClicksChanged() { }

		void StartFinish() {
			IsAlive = false;
			OnFinishStarted();
		}
	}
}

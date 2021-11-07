using UnityEngine;
using UnityEngine.EventSystems;

using System;
using System.Collections.Generic;

namespace SGJ.Core.Toilet {
	[RequireComponent(typeof(Collider2D))]
	public abstract class BaseToiletPart : MonoBehaviour, IPointerClickHandler {
		public static readonly HashSet<BaseToiletPart> Instances = new HashSet<BaseToiletPart>();

		public static event Action OnToiletPartDestroyed;

		[Header("Parameters")]
		public int TotalClicks;

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
			Destroy(gameObject);
		}

		protected abstract void OnFinishStarted();

		protected virtual void OnCurClicksChanged() { }

		void StartFinish() {
			IsAlive = false;
			OnFinishStarted();
		}
	}
}

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

		protected int  CurClicks { get; set; }
		protected bool IsAlive   { get; set; } = true;

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
			if ( !IsAlive ) {
				return;
			}
			++CurClicks;
			OnCurClicksChanged();
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

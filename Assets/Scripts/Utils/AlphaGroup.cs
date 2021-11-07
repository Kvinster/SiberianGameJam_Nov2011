using UnityEngine;

using System.Collections.Generic;

using Shapes;

using TMPro;

namespace SGJ.Utils {
	[ExecuteInEditMode]
	public sealed class AlphaGroup : MonoBehaviour {
		[Header("Parameters")]
		[Range(0f, 1f)]
		public float Alpha = 1f;
		[Header("Dependencies")]
		public List<ShapeRenderer> ShapeRenderers;
		public List<SpriteRenderer> SpriteRenderers;
		public List<TMP_Text>       Texts;
		public List<CanvasGroup>    CanvasGroups;

		float _prevAlpha;

		void Reset() {
			if ( ShapeRenderers == null ) {
				ShapeRenderers = new List<ShapeRenderer>();
			}
			GetComponentsInChildren(ShapeRenderers);
			if ( SpriteRenderers == null ) {
				SpriteRenderers = new List<SpriteRenderer>();
			}
			GetComponentsInChildren(SpriteRenderers);
			if ( Texts == null ) {
				Texts = new List<TMP_Text>();
			}
			GetComponentsInChildren(Texts);
			if ( CanvasGroups == null ) {
				CanvasGroups = new List<CanvasGroup>();
			}
			GetComponentsInChildren(CanvasGroups);
		}

		void Start() {
			if ( Application.isPlaying ) {
				_prevAlpha = Alpha;
			}
		}

		void Update() {
			if ( !Application.isPlaying || !Mathf.Approximately(_prevAlpha, Alpha) ) {
				foreach ( var sr in ShapeRenderers ) {
					TryChangeShapeRendererAlpha(sr, Alpha);
				}
				foreach ( var sr in SpriteRenderers ) {
					TryChangeSpriteRendererAlpha(sr, Alpha);
				}
				foreach ( var text in Texts ) {
					TryChangeTextAlpha(text, Alpha);
				}
				foreach ( var cg in CanvasGroups ) {
					TryChangeCanvasGroupAlpha(cg, Alpha);
				}
				if ( Application.isPlaying ) {
					_prevAlpha = Alpha;
				}
			}
		}

		void TryChangeShapeRendererAlpha(ShapeRenderer sr, float alpha) {
			var curAlpha = sr.Color.a;
			if ( !Mathf.Approximately(curAlpha, alpha) ) {
				sr.Color = new Color(sr.Color.r, sr.Color.g, sr.Color.b, alpha);
#if UNITY_EDITOR
				if ( !Application.isPlaying ) {
					UnityEditor.EditorUtility.SetDirty(sr);
				}
#endif
			}
		}

		void TryChangeSpriteRendererAlpha(SpriteRenderer sr, float alpha) {
			var curAlpha = sr.color.a;
			if ( !Mathf.Approximately(curAlpha, alpha) ) {
				sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
#if UNITY_EDITOR
				if ( !Application.isPlaying ) {
					UnityEditor.EditorUtility.SetDirty(sr);
				}
#endif
			}
		}

		void TryChangeTextAlpha(TMP_Text text, float alpha) {
			var curAlpha = text.color.a;
			if ( !Mathf.Approximately(curAlpha, alpha) ) {
				text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
#if UNITY_EDITOR
				if ( !Application.isPlaying ) {
					UnityEditor.EditorUtility.SetDirty(text);
				}
#endif
			}
		}

		void TryChangeCanvasGroupAlpha(CanvasGroup canvasGroup, float alpha) {
			var curAlpha = canvasGroup.alpha;
			if ( !Mathf.Approximately(curAlpha, alpha) ) {
				canvasGroup.alpha = Alpha;
#if UNITY_EDITOR
				if ( !Application.isPlaying ) {
					UnityEditor.EditorUtility.SetDirty(canvasGroup);
				}
#endif
			}
		}
	}
}

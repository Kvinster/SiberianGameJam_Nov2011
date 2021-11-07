using UnityEngine;

using Shapes;

namespace SGJ.Core.Toilet {
	public sealed class ToiletWaterView : MonoBehaviour {
		[Header("Parameters")]
		public float MinHeight;
		public float MaxHeight;
		[Header("Dependencies")]
		public ToiletLevelManager LevelManager;
		public Rectangle Fill;
		public Transform BobberTransform;
		public Transform BobberStartPos;
		public Transform BobberEndPos;

		void Update() {
			Fill.Height = Mathf.Lerp(MinHeight, MaxHeight, LevelManager.LevelProgress);
			BobberTransform.position =
				Vector3.Lerp(BobberStartPos.position, BobberEndPos.position, LevelManager.LevelProgress);
		}
	}
}

using UnityEngine;

using SGJ.Common;

namespace SGJ.Meta {
	public sealed class Room : MonoBehaviour {
		public RoomType  RoomType = RoomType.Unknown;
		public Transform ZoomPoint;
	}
}

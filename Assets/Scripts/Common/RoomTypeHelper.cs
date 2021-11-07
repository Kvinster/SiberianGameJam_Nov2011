using UnityEngine;

namespace SGJ.Common {
	public static class RoomTypeHelper {
		public static string RoomTypeToSceneName(RoomType roomType) {
			switch ( roomType ) {
				case RoomType.Kitchen:  return "Kitchen";
				case RoomType.Bathroom: return "Bathroom";
				case RoomType.Toilet:   return "Toilet";
				default: {
					Debug.LogErrorFormat("RoomTypeHelper.RoomTypeToSceneName: unsupported room type '{0}'",
						roomType.ToString());
					return null;
				}
			}
		}
	}
}

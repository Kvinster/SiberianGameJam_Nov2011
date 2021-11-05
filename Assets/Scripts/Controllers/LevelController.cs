using UnityEngine;

using SGJ.Common;

namespace SGJ.Controllers {
	public class LevelController {
		static readonly RoomType[] RoomsOrder = new [] {
			RoomType.Kitchen, RoomType.LivingRoom, RoomType.Bathroom, RoomType.Entrance, RoomType.Bedroom
		};

		static LevelController _instance;

		public static LevelController Instance => _instance ?? (_instance = new LevelController());

		public int NextLevelIndex { get; private set; } = 0;

		public int PrevLevelIndex => NextLevelIndex - 1;

		public RoomType NextLevelType => RoomsOrder[NextLevelIndex];

		public RoomType PrevLevelType {
			get {
				if ( (PrevLevelIndex < 0) || (PrevLevelIndex >= RoomsOrder.Length) ) {
					return RoomType.Unknown;
				}
				return RoomsOrder[PrevLevelIndex];
			}
		}

		public void ResetProgress() {
			NextLevelIndex = 0;
		}

		public void TryAdvance() {
			NextLevelIndex = Mathf.Clamp(NextLevelIndex + 1, 0, RoomsOrder.Length);
		}
	}
}

using SGJ.Common;

namespace SGJ.Controllers {
	public class LevelController {
		static readonly RoomType[] RoomsOrder = {
			RoomType.Kitchen,
			RoomType.Bathroom,
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

		public bool IsGameWon { get; private set; }

		public void ResetProgress() {
			NextLevelIndex = 0;
			IsGameWon      = false;
		}

		public void TryAdvance() {
			++NextLevelIndex;
			if ( NextLevelIndex >= RoomsOrder.Length ) {
				NextLevelIndex = RoomsOrder.Length - 1;
				IsGameWon      = true;
			}
		}
	}
}

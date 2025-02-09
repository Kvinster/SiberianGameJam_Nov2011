﻿using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;

using SGJ.Common;
using SGJ.Controllers;

using DG.Tweening;

namespace SGJ.Meta {
	public sealed class MetaManager : MonoBehaviour {
		[Header("Parameters")]
		public float   PauseTime;
		public float   ZoomTime;
		public float   CameraStartSize = 5f;
		public float   CameraZoomSize;
		public Vector3 CameraStartPos;
		[Header("Dependencies")]
		public Camera Camera;
		public Transform    CameraTransform;
		public Room[]       Rooms;
		public GameObject   WinScreen;
		public GameObject   ProgressBarRoot;
		public GameObject[] ProgressBarElements;

		bool _isWinScreenShown;

		void Start() {
			if ( LevelController.Instance.IsGameWon ) {
				AnimAndLoadVictory();
			} else {
				AnimAndLoadNextRoom();
			}
		}

		void Update() {
			if ( _isWinScreenShown && Input.anyKey ) {
				LoadMainMenu();
			}
		}

		void AnimAndLoadVictory() {
			WinScreen.SetActive(true);
			ProgressBarRoot.SetActive(false);
			CameraTransform.position = CameraStartPos;
			Camera.orthographicSize  = CameraStartSize;

			_isWinScreenShown = true;
		}

		void AnimAndLoadNextRoom() {
			WinScreen.SetActive(false);

			var lc = LevelController.Instance;

			ProgressBarRoot.SetActive(true);
			for ( var i = 0; i < ProgressBarElements.Length; ++i ) {
				var element = ProgressBarElements[i];
				element.SetActive(i == lc.NextLevelIndex);
			}

			var roomType = lc.NextLevelType;
			var room     = Rooms.FirstOrDefault(x => x.RoomType == roomType);
			if ( !room ) {
				Debug.LogErrorFormat("MetaManager.Start: can't find next room with type '{0}'", roomType.ToString());
				return;
			}

			var progress = 0f;
			var sequence = DOTween.Sequence();
			if ( lc.PrevLevelIndex >= 0 ) {
				var prevRoom = Rooms.FirstOrDefault(x => x.RoomType == lc.PrevLevelType);
				if ( !prevRoom ) {
					Debug.LogErrorFormat("MetaManager.Start: can't find prev room with type '{0}'",
						roomType.ToString());
					return;
				}
				CameraTransform.position = prevRoom.ZoomPoint.position;
				Camera.orthographicSize  = CameraZoomSize;
				progress                 = 1f;
				sequence.Append(DOTween.To(() => progress, x => {
					progress                 = x;
					Camera.orthographicSize  = Mathf.Lerp(CameraStartSize, CameraZoomSize, x);
					CameraTransform.position = Vector3.Lerp(CameraStartPos, prevRoom.ZoomPoint.position, x);
				}, 0f, ZoomTime));
			}

			sequence.AppendInterval(PauseTime)
				.Append(DOTween.To(() => progress, x => {
					progress                 = x;
					Camera.orthographicSize  = Mathf.Lerp(CameraStartSize, CameraZoomSize, x);
					CameraTransform.position = Vector3.Lerp(CameraStartPos, room.ZoomPoint.position, x);
				}, 1f, ZoomTime))
				.OnComplete(LoadNextRoom);
		}

		void LoadMainMenu() {
			SceneManager.LoadScene("MainMenu");
		}

		void LoadNextRoom() {
			var roomType  = LevelController.Instance.NextLevelType;
			var sceneName = RoomTypeHelper.RoomTypeToSceneName(roomType);
			if ( !string.IsNullOrEmpty(sceneName) ) {
				SceneManager.LoadScene(sceneName);
			} else {
				Debug.LogErrorFormat("MetaManager.LoadNextRoom: can't find scene name for room '{0}'",
					roomType.ToString());
			}
		}
	}
}

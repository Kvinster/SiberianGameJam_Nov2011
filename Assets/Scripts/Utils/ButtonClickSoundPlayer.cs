using UnityEngine;
using UnityEngine.UI;

using SGJ.Service;

namespace SGJ.Utils {
	[RequireComponent(typeof(Button))]
	public sealed class ButtonClickSoundPlayer : MonoBehaviour {
		public AudioClip Clip;

		Button _button;

		Button Button {
			get {
				if ( !_button ) {
					_button = GetComponent<Button>();
				}
				return _button;
			}
		}

		void OnEnable() {
			Button.onClick.AddListener(OnButtonClick);
		}

		void OnDisable() {
			Button.onClick.RemoveListener(OnButtonClick);
		}

		void OnButtonClick() {
			AudioService.PlaySound(Clip);
		}
	}
}

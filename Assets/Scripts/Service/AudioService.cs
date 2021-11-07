using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace SGJ.Service {
	public static class AudioService {
		static GameObject  _gameObject;
		static AudioSource _audioSource;

		static readonly Dictionary<object, AudioSource> Pool = new Dictionary<object, AudioSource>();

		static GameObject GameObject {
			get {
				if ( !_gameObject ) {
					_gameObject = new GameObject("[AudioService]");
					Object.DontDestroyOnLoad(_gameObject);
				}
				return _gameObject;
			}
		}

		static AudioSource AudioSource {
			get {
				if ( !_audioSource ) {
					_audioSource = GameObject.AddComponent<AudioSource>();
				}
				return _audioSource;
			}
		}

		public static void PlaySound(AudioClip clip) {
			Assert.IsTrue(clip);
			AudioSource.PlayOneShot(clip);
		}

		public static void PlayInPool(object key, AudioClip clip, bool loop) {
			Assert.IsNotNull(key);
			Assert.IsFalse(Pool.ContainsKey(key));
			var audioSource = GameObject.AddComponent<AudioSource>();
			audioSource.clip = clip;
			audioSource.loop = loop;
			audioSource.Play();
			Pool.Add(key, audioSource);
		}

		public static void StopInPool(object key) {
			Assert.IsNotNull(key);
			if ( Pool.TryGetValue(key, out var audioSource) && audioSource ) {
				audioSource.Stop();
				Object.Destroy(audioSource);
				Pool.Remove(key);
			}
		}

		public static void SetVolumeInPool(object key, float volumeScale) {
			Assert.IsNotNull(key);
			if ( Pool.TryGetValue(key, out var audioSource) && audioSource ) {
				audioSource.volume = Mathf.Clamp01(volumeScale);
			}
		}

		public static float GetVolumeInPool(object key) {
			Assert.IsNotNull(key);
			if ( Pool.TryGetValue(key, out var audioSource) && audioSource ) {
				return audioSource.volume;
			}
			return -1f;
		}
	}
}

using UnityEngine;

namespace SGJ.Core.Kitchen {
	public sealed class SandwichSpawner : MonoBehaviour {
		[Header("Parameters")]
		public float SpawnDelay;
		[Header("Dependencies")]
		public GameObject SandwichPrefab;
		public Transform StartSpawnPoint;
		public Transform EndSpawnPoint;

		float _timer;

		public bool IsActive { get; set; }

		void Update() {
			if ( !IsActive ) {
				return;
			}
			_timer += Time.deltaTime;
			if ( _timer >= SpawnDelay ) {
				_timer -= SpawnDelay;
				Spawn();
			}
		}

		void Spawn() {
			var pos = Vector3.Lerp(StartSpawnPoint.position, EndSpawnPoint.position, Random.Range(0f, 1f));
			Instantiate(SandwichPrefab, pos, Quaternion.identity);
		}
	}
}

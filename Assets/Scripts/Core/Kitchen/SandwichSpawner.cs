using UnityEngine;

namespace SGJ.Core.Kitchen {
	public sealed class SandwichSpawner : MonoBehaviour {
		[Header("Parameters")]
		public int TotalSandwiches = 5;
		public float SpawnDelay;
		[Header("Dependencies")]
		public GameObject SandwichPrefab;
		public Transform StartSpawnPoint;
		public Transform EndSpawnPoint;

		float _timer;

		int _totalSpawned;

		public bool IsActive { get; set; }

		bool CanSpawn => (_totalSpawned < TotalSandwiches);

		void Update() {
			if ( !IsActive ) {
				return;
			}
			_timer += Time.deltaTime;
			if ( _timer >= SpawnDelay ) {
				_timer -= SpawnDelay;
				TrySpawn();
			}
		}

		void TrySpawn() {
			if ( !CanSpawn ) {
				return;
			}
			var pos = Vector3.Lerp(StartSpawnPoint.position, EndSpawnPoint.position, Random.Range(0f, 1f));
			Instantiate(SandwichPrefab, pos, Quaternion.identity);
			++_totalSpawned;
		}
	}
}

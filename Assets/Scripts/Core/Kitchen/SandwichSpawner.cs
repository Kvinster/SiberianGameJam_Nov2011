using UnityEngine;
using UnityEngine.Assertions;

namespace SGJ.Core.Kitchen {
	public sealed class SandwichSpawner : MonoBehaviour {
		[Header("Parameters")]
		public float SpawnDelay;
		public float MinSandwichGravityScale = 0.5f;
		public float MaxSandwichGravityScale = 0.8f;
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
			var pos        = Vector3.Lerp(StartSpawnPoint.position, EndSpawnPoint.position, Random.Range(0f, 1f));
			var sandwichGo = Instantiate(SandwichPrefab, pos, Quaternion.identity);
			var sandwichRb = sandwichGo.GetComponent<Rigidbody2D>();
			Assert.IsTrue(sandwichRb);
			sandwichRb.gravityScale = Random.Range(MinSandwichGravityScale, MaxSandwichGravityScale);
		}
	}
}

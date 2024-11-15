using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

	private void OnEnable()
	{
		_cubeSpawner.CubeReleasing += SpawnInPosition;
	}
	
	private void OnDisable()
	{
		_cubeSpawner.CubeReleasing -= SpawnInPosition;
	}
	
	private void SpawnInPosition(Vector3 position, Vector3 velocity)
	{
		Spawn(position, velocity);
	}
}
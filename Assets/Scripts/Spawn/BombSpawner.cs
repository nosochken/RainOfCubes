using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;
	
	private Vector3 _spawnPosition;
	private Vector3 _bombVelocity;
	
	private void OnEnable()
	{
		_cubeSpawner.CubeReleasing += SpawnInPosition;
	}
	
	private void OnDisable()
	{
		_cubeSpawner.CubeReleasing -= SpawnInPosition;
	}

	protected override void ActOnGet(Bomb bomb)
	{
		bomb.transform.position = _spawnPosition;
		
		if(bomb.TryGetComponent(out Rigidbody rigidbody))
			rigidbody.velocity = _bombVelocity;
		
		base.ActOnGet(bomb);
	}
	
	private void SpawnInPosition(Vector3 position, Vector3 velocity)
	{
		_spawnPosition = position;
		_bombVelocity = velocity;
		
		Spawn();
	}
}
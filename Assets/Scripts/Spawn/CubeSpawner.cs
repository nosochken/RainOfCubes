using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private KeyCode _spawnStopKey = KeyCode.Space;
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField, Min(0)] private float _delay = 0.5f;

	private Coroutine _coroutine;
	
	public event Action<Vector3, Vector3> CubeReleasing;

	private void Start()
	{
		_coroutine = StartCoroutine(SpawnWithDelay());
	}

	private void Update()
	{
		if (Input.GetKeyDown(_spawnStopKey))
		{
			if (_coroutine != null)
				StopCoroutine(_coroutine);
		}
	}
	
	protected override void ReturnToPool(Cube cube)
	{
		if (cube.TryGetComponent(out Rigidbody rigidbody))
			CubeReleasing?.Invoke(cube.transform.position, rigidbody.velocity);
		else
			CubeReleasing?.Invoke(cube.transform.position, Vector3.zero);
		
		base.ReturnToPool(cube);
	}

	private IEnumerator SpawnWithDelay()
	{
		var wait = new WaitForSeconds(_delay);
		
		while (!Input.GetKey(_spawnStopKey))
		{ 
			yield return wait;
			
			Vector3 position = _spawnZone.GetRandomPosition(SpawnableObjectScale);
			Spawn(position, Vector3.zero);
		}
	}
}
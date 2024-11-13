using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private T _prefab;
    [SerializeField, Min(1)] private int _poolCapacity = 15;
	[SerializeField, Min(1)] private int _poolMaxSize = 15;

	private ObjectPool<T> _pool;
	
	private int _createdObjectsAmount;
	private int _spawnedObjectsAmount;
	
	public event Action<int> CreatedObjectsAmountHasChanged;
	public event Action<int> SpawnedObjectsAmountHasChanged;
	public event Action<int> ActiveObjectsAmountHasChanged;

	private void Awake()
	{
		_pool = new ObjectPool<T>(
		createFunc: () => Create(),
		actionOnGet: (spawnableObject) => ActOnGet(spawnableObject),
		actionOnRelease : (spawnableObject) => spawnableObject.ResetSettings(),
		actionOnDestroy : (spawnableObject) => ActOnDestroy(spawnableObject),
		collectionCheck : true,
		defaultCapacity : _poolCapacity,
		maxSize : _poolMaxSize);
	}
	
	protected void Spawn()
	{
		_pool.Get();
		
		_spawnedObjectsAmount++;
		SpawnedObjectsAmountHasChanged?.Invoke(_spawnedObjectsAmount);
	}
	
	protected virtual void ActOnGet(T spawnableObject)
	{
		spawnableObject.gameObject.SetActive(true);
		
		ActiveObjectsAmountHasChanged?.Invoke(_pool.CountActive);
	}
	
	protected virtual void ReturnToPool(T spawnableObject)
	{
		spawnableObject.gameObject.SetActive(false);
		
		ActiveObjectsAmountHasChanged?.Invoke(_pool.CountActive);
		 
		_pool.Release(spawnableObject);
	}

	private T Create()
	{
		T spawnableObject = Instantiate(_prefab);
		spawnableObject.ReadiedForRelease += ReturnToPool;
		
		_createdObjectsAmount++;
		CreatedObjectsAmountHasChanged?.Invoke(_createdObjectsAmount);

		return spawnableObject;
	}

	private void ActOnDestroy(T spawnableObject)
	{
		spawnableObject.ReadiedForRelease -= ReturnToPool;
		ActiveObjectsAmountHasChanged?.Invoke(_pool.CountActive);

		Destroy(spawnableObject.gameObject);
	}
}
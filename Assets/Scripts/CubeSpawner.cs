using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Plane _plane;

    [SerializeField, Min(1)] private int _poolCapacity = 5;
    [SerializeField, Min(1)] private int _poolMaxSize = 5;

    [SerializeField, Min(0)] private float _delay = 2f;
    [SerializeField, Min(5)] private float _spawnHeight = 15f;
    [SerializeField] private KeyCode _spawnStopKey = KeyCode.Space;

    private ObjectPool<Cube> _pool;
    private Coroutine _coroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Create(),
        actionOnGet: (cube) => ActOnGet(cube),
        actionOnRelease : (cube) => cube.ResetSettings(),
        actionOnDestroy : (cube) => ActOnDestroy(cube),
        collectionCheck : true,
        defaultCapacity : _poolCapacity,
        maxSize : _poolMaxSize);
    }

    private void Start()
    {
        Debug.Log($"to stop spawning press {_spawnStopKey}");

        _coroutine = StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(_spawnStopKey))
        {
            StopCoroutine(_coroutine);

            Debug.Log("spawn completed");
        }
    }

    private Cube Create()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.LifetimeWasOver += ReturnToPoolAfterLifetime;

        return cube;
    }

    private void ActOnGet(Cube cube)
    {
        cube.gameObject.transform.position =
            _plane.GetRandomPosition(_spawnHeight, cube.transform.localScale);

        cube.gameObject.SetActive(true);  
    }

    private void ActOnDestroy(Cube cube)
    {
        cube.LifetimeWasOver -= ReturnToPoolAfterLifetime;

        Destroy(cube.gameObject);
    }

    private IEnumerator Spawn()
    {
        while (!Input.GetKey(_spawnStopKey))
        {
            var wait = new WaitForSecondsRealtime(_delay);
            yield return wait;

            _pool.Get();
        }
    }

    private void ReturnToPoolAfterLifetime(Cube cube)
    {
        _pool.Release(cube);
    }
}
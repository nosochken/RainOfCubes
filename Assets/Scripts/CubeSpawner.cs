using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Plane _plane;

    [SerializeField, Min(1)] private int _poolCapacity = 5;
    [SerializeField, Min(1)] private int _poolMaxSize = 5;

    [SerializeField, Min(0)] private float _delay = 5f;
    [SerializeField, Min(0)] private float _repeatRate = 1f;

    [SerializeField, Min(5)] private float _spawnHeight = 15f;
    [SerializeField] private KeyCode _spawnStopKey = KeyCode.Space;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease : (cube) => cube.ResetSettings(),
        actionOnDestroy : (cube) => Destroy(cube.gameObject),
        collectionCheck : true,
        defaultCapacity : _poolCapacity,
        maxSize : _poolMaxSize);
    }

    private void Start()
    {
        Debug.Log($"to stop spawning press {_spawnStopKey}");

        InvokeRepeating(nameof(SpawnCube), _delay, _repeatRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_spawnStopKey))
        {
            CancelInvoke(nameof(SpawnCube));

            Debug.Log("spawn completed");
        }
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.transform.position =
            _plane.GetRandomPosition(_spawnHeight, cube.transform.localScale);

        cube.gameObject.SetActive(true);
    }

    private void SpawnCube()
    {
        StartCoroutine(ReturnToPoolAfterLifetime(_pool.Get()));
    }

   private IEnumerator ReturnToPoolAfterLifetime(Cube cube)
    {
        yield return new WaitUntil(() => cube.IsLifetimeOver);

        _pool.Release(cube);
    }
}
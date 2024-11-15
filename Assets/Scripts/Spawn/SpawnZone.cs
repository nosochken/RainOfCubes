using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private Plane _plane;
    [SerializeField, Min(1)] private float _height = 15f;

    private Collider _planeCollider;

    private void Awake()
    {
        _planeCollider = _plane.GetComponent<Collider>();
    }

    private void Start()
    {
        gameObject.transform.position =
            new Vector3(_plane.transform.position.x,
            _height, _plane.transform.position.z);
    }

    public Vector3 GetRandomPosition(Vector3 objectScale)
    {
        float x = Random.Range(
            _planeCollider.bounds.min.x + objectScale.x,
            _planeCollider.bounds.max.x - objectScale.x);
        float z = Random.Range(
            _planeCollider.bounds.min.z + objectScale.z,
            _planeCollider.bounds.max.z - objectScale.z);

        return new Vector3(x, _height, z);
    }
}
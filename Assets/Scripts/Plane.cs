using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
public class Plane : MonoBehaviour
{
    private Collider _collider;
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void Start()
    {
        _colorChanger.Change();
    }

    public Vector3 GetRandomPosition(float y, Vector3 objectScale)
    {
        float x = Random.Range(
            _collider.bounds.min.x + objectScale.x,
            _collider.bounds.max.x - objectScale.x);
        float z = Random.Range(
            _collider.bounds.min.z + objectScale.z,
            _collider.bounds.max.z - objectScale.z);

        return new Vector3(x, y, z);
    }
}
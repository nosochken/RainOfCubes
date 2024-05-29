using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private Dyeing _dyeing;

    private void Start()
    {
        _dyeing.Dye();
    }

    public Vector3 GetRandomPosition(float y, Vector3 objectScale)
    {
        float x = Random.Range(
            GetComponent<Collider>().bounds.min.x + objectScale.x,
            GetComponent<Collider>().bounds.max.x - objectScale.x);
        float z = Random.Range(
            GetComponent<Collider>().bounds.min.z + objectScale.z,
            GetComponent<Collider>().bounds.max.z - objectScale.z);

        return new Vector3(x, y, z);
    }
}
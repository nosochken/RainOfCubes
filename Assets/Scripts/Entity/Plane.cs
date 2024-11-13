using UnityEngine;

[RequireComponent(typeof(Collider), typeof(ColorChanger))]
public class Plane : MonoBehaviour
{
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void Start()
    {
        _colorChanger.Change();
    }
}
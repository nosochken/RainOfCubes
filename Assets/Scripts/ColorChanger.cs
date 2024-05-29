using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void ChangeColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}
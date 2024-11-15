using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;

    public void Init()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void Change()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetDefault()
    {
        _renderer.material.color = _defaultColor;
    }
}
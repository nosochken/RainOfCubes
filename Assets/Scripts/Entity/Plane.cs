using UnityEngine;

[RequireComponent(typeof(Collider), typeof(ColorChanger))]
public class Plane : MonoBehaviour
{
    private ColorChanger _colorChanger;

    private void Awake()
	{
		_colorChanger = GetComponent<ColorChanger>();
		_colorChanger.Init();
	}

	private void Start()
	{
		_colorChanger.Change();
	}
}
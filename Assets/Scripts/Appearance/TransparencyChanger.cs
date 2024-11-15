using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TransparencyChanger : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _targetOpacity = 0f;

    private Material _material;
    private Color _startColor;
	private Color _targetColor;
	
	public void Init()
	{
		_material = GetComponent<Renderer>().material;
		
		_startColor = _material.color;
		_targetColor = new Color(_startColor.r, _startColor.g, _startColor.b, _targetOpacity);
	}
	
	public void BecomeTransparent(float elapsedTime, float lifeTime)
	{
		_material.color = Color.Lerp(_startColor, _targetColor, elapsedTime / lifeTime);
	}
	
	public void SetInitialTransparency()
	{
		_material.color = _startColor;
	}
}
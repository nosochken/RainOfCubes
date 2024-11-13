using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TransparencyChanger : MonoBehaviour
{
    private Material _material;
    private Color _startColor;

    private void Awake()
	{
		_material = GetComponent<Renderer>().material;
		_startColor = _material.color;
	}
	
	public IEnumerator GraduallyBecomeTransparent(float lifeTimer)
	{
		float elapsedTime = 0f;
		
		float opacity = 0f;
		Color targetColor = new Color(_startColor.r, _startColor.g, _startColor.b, opacity);
		
		while (elapsedTime < lifeTimer)
		{
			_material.color = Color.Lerp(_startColor, targetColor, elapsedTime / lifeTimer);
			elapsedTime += Time.deltaTime;
			
			yield return null;
		}
	}
	
	public void SetInitialTransparency()
	{
		_material.color = _startColor;
	}
}
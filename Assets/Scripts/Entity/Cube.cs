using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifetimeDeterminant), typeof(ColorChanger))]
public class Cube : MonoBehaviour, ISpawnable<Cube>
{
    [SerializeField, Min(1)] private float _timeUntilLifelessCycleRelease = 5f;

	private LifetimeDeterminant _lifetimeDeterminant;
	private ColorChanger _colorChanger;

	private bool _hadCollision;
	
	public event Action<Cube> ReadiedForRelease;

	private void Awake()
	{
		_lifetimeDeterminant = GetComponent<LifetimeDeterminant>();
		
		_colorChanger = GetComponent<ColorChanger>();
		_colorChanger.Init();
	}
	
	private void OnEnable()
	{
		_lifetimeDeterminant.DetermineLifetime();
		StartCoroutine(BeReadyForReleaseAfterTime());
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (_hadCollision == false)
		{
			if (collision.gameObject.TryGetComponent<Plane>(out _))
				StartCoroutine(BeActive());
		}  
	}

	public void ResetSettings()
	{
		_hadCollision = false;
		_colorChanger.SetDefault();
	}
	
	private IEnumerator BeReadyForReleaseAfterTime()
	{
		yield return new WaitForSeconds(_timeUntilLifelessCycleRelease);
		
		if(_hadCollision == false)
			ReadiedForRelease?.Invoke(this);
	}

	private IEnumerator BeActive()
	{
		_hadCollision = true;
		_colorChanger.Change();
		
		yield return new WaitForSeconds(_lifetimeDeterminant.Lifetime);
		
		ReadiedForRelease?.Invoke(this);
	}
}
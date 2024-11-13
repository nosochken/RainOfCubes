using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifeTimer), typeof(ColorChanger))]
public class Cube : MonoBehaviour, ISpawnable<Cube>
{
    [SerializeField] private float _timeUntilLifelessCycleRelease = 5f;

    private LifeTimer _lifeTimer;
    private ColorChanger _colorChanger;
	
	private bool _hadCollision;
	
	public event Action<Cube> ReadiedForRelease;

	private void Awake()
	{
		_lifeTimer = GetComponent<LifeTimer>();
		_colorChanger = GetComponent<ColorChanger>();
	}
	
	private void OnEnable()
	{
		_lifeTimer.LifetimeWasOver += ReportReadinessForRelease;
		
		StartCoroutine(BeReadyForReleaseAfterTime());
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (_hadCollision == false)
		{
			if (collision.gameObject.GetComponent<Plane>())
				ActivateLifeCycle();
		}  
	}
	
	private void OnDisable()
	{
		_lifeTimer.LifetimeWasOver -= ReportReadinessForRelease;
	}

	public void ResetSettings()
	{
		_hadCollision = false;
		_colorChanger.SetDefault();
	}

	private void ActivateLifeCycle()
	{
		_hadCollision = true;
		_colorChanger.Change();
		
		StartCoroutine(_lifeTimer.Expire());
	}
	
	private IEnumerator BeReadyForReleaseAfterTime()
	{
		yield return StartCoroutine(_lifeTimer.ExpireThrough(_timeUntilLifelessCycleRelease));
		
		if(!_hadCollision)
			ReportReadinessForRelease();
	}
	
	private void ReportReadinessForRelease() => ReadiedForRelease?.Invoke(this);
}
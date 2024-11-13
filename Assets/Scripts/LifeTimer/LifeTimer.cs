using System;
using System.Collections;
using UnityEngine;

public class LifeTimer : Timer
{
    [SerializeField, Min(1)] private int _minLifetime = 2;
	[SerializeField, Min(1)] private int _maxLifetime = 5;
	
	private float _lifetime;

	public event Action LifetimeWasOver;
	
	public float Lifetime => _lifetime;
	
	private void OnEnable()
	{
		_lifetime = DetermineLifetime();
	}
	
	public IEnumerator Expire()
	{
		yield return ExpireThrough(_lifetime);

		LifetimeWasOver?.Invoke();
	}
	
	private int DetermineLifetime() => UnityEngine.Random.Range(_minLifetime, _maxLifetime);
}
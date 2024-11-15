using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifetimeDeterminant), typeof(TransparencyChanger))]
public class Bomb : MonoBehaviour, ISpawnable<Bomb>
{
    [SerializeField, Min(0)] private float _explosionRadius = 10f;
    [SerializeField, Min(0)] private float _explosionForce = 50f;
	[SerializeField, Min(0)] private float _upwardsModifier = 3f;

	private LifetimeDeterminant _lifetimeDeterminant;
	private TransparencyChanger _transparencyChanger;
   
	public event Action<Bomb> ReadiedForRelease;
	
	private void Awake()
	{
		_lifetimeDeterminant = GetComponent<LifetimeDeterminant>();
		
		_transparencyChanger = GetComponent<TransparencyChanger>();
		_transparencyChanger.Init();
	}
	
	private void OnEnable()
	{
		_lifetimeDeterminant.DetermineLifetime();
		StartCoroutine(BeActive());
	}
	
	public void ResetSettings() => _transparencyChanger.SetInitialTransparency();
	
	private IEnumerator BeActive()
	{
		float elapsedTime = 0;
		
		while (elapsedTime < _lifetimeDeterminant.Lifetime)
		{
			_transparencyChanger.BecomeTransparent(elapsedTime, _lifetimeDeterminant.Lifetime);
			elapsedTime += Time.deltaTime;
			
			yield return null;
		}
		
		Explode();
	}
	
	private void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
		
		foreach (Collider collider in colliders)
		{
			if(collider.TryGetComponent(out Rigidbody rigidbody))
				rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
		}
		
		ReadiedForRelease?.Invoke(this);
	}
}
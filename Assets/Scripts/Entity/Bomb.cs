using System;
using UnityEngine;

[RequireComponent(typeof(LifeTimer), typeof(TransparencyChanger))]
public class Bomb : MonoBehaviour, ISpawnable<Bomb>
{
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _upwardsModifier = 3f;

	private LifeTimer _lifeTimer;
	private TransparencyChanger _transparencyChanger;
   
	public event Action<Bomb> ReadiedForRelease;
	
	private void Awake()
	{
		_lifeTimer = GetComponent<LifeTimer>();
		_transparencyChanger = GetComponent<TransparencyChanger>();
	}
	
	private void OnEnable()
	{
		_lifeTimer.LifetimeWasOver += Explode;
		
		StartCoroutine(_lifeTimer.Expire());
		StartCoroutine(_transparencyChanger.GraduallyBecomeTransparent(_lifeTimer.Lifetime));
	}
	
	private void OnDisable()
	{
		_lifeTimer.LifetimeWasOver -= Explode;
	}
	
	public void ResetSettings() => _transparencyChanger.SetInitialTransparency();
	
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
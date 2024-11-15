using UnityEngine;

public class LifetimeDeterminant : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minLifetime = 2;
    [SerializeField, Min(1)] private int _maxLifetime = 5;
	
	public float Lifetime {get; private set;}
	
	public void DetermineLifetime()
    {
    	Lifetime = Random.Range(_minLifetime, _maxLifetime);
    }
}
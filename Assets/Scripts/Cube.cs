using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minLifetime = 2;
    [SerializeField, Min(1)] private int _maxLifetime = 5;

    private ColorChanger _colorChanger;
    private bool _hadCollision;
    private int _lifetime;

    public event Action<Cube> LifetimeWasOver;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hadCollision == false)
        {
            if (collision.gameObject.GetComponent<Plane>())
                ActivateLifeCycle();
        }  
    }

    public void ResetSettings()
    {
        _hadCollision = false;
        _colorChanger.SetDefault();
        gameObject.SetActive(false);
    }

    private void ActivateLifeCycle()
    {
        _hadCollision = true;
        _lifetime = DetermineLifetime();
        _colorChanger.Change();

        StartCoroutine(Expire());
    }

    private int DetermineLifetime()
    {
        return UnityEngine.Random.Range(_minLifetime, _maxLifetime);
    }

    private IEnumerator Expire()
    {
        var wait = new WaitForSecondsRealtime(_lifetime);
        yield return wait;

        LifetimeWasOver?.Invoke(this);
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minLifetime = 2;
    [SerializeField, Min(1)] private int _maxLifetime = 5;

    private ColorChanger _colorChanger;
    private bool _hadCollision;
    private bool _isLifetimeOver;
    private int _lifetime;

    public bool IsLifetimeOver => _isLifetimeOver;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Plane>())
        {
            if (_hadCollision == false)
                ActivateLifeCycle();
        }  
    }

    public void ResetSettings()
    {
        _hadCollision = false;
        _colorChanger.SetDefaultColor();
        gameObject.SetActive(false);
        _isLifetimeOver = false;
    }

    private void ActivateLifeCycle()
    {
        _hadCollision = true;
        _lifetime = DetermineLifetime();
        _colorChanger.ChangeColor();

        StartCoroutine(Expire());
    }

    private int DetermineLifetime()
    {
        return Random.Range(_minLifetime, _maxLifetime);
    }

    private IEnumerator Expire()
    {
        var wait = new WaitForSecondsRealtime(_lifetime);
        yield return wait;

        _isLifetimeOver = true;
    }
}
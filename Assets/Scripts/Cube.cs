using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Dyeing _dyeing;

    [SerializeField, Min(1)] private int _minLifetime = 2;
    [SerializeField, Min(1)] private int _maxLifetime = 5;

    private bool _hadCollision;
    private bool _isLifetimeOver;
    private int _lifetime;

    public bool IsLifetimeOver => _isLifetimeOver;

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
        _dyeing.SetDefaultColor();
        gameObject.SetActive(false);
        _isLifetimeOver = false;
    }

    private void ActivateLifeCycle()
    {
        _hadCollision = true;
        _lifetime = DetermineLifetime();
        _dyeing.Dye();

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
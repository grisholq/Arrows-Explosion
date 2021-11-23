using UnityEngine;
using System.Collections.Generic;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private bool _enableOnAwake;

    public IEnumerable<Rigidbody> Rigidbodies => _parts;

    public float AverageVelocity
    {
        get
        {
            float sum = 0;
            foreach (var part in _parts)
            {
                sum += part.velocity.magnitude;
            }
            return sum / _parts.Length;
        }
    }

    protected Rigidbody[] _parts;

    private void Awake()
    {
        Inizialize();
    }

    protected virtual void Inizialize()
    {
        _parts = GetComponentsInChildren<Rigidbody>();

        if (_enableOnAwake == true) Enable();
        else Disable();
    }

    public void Enable()
    {
        foreach (var part in _parts)
        {
            part.useGravity = true;
        }
    }

    public void Disable()
    {
        foreach (var part in _parts)
        {
            part.useGravity = false;        
        }
    }
}
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
    
    public float MaxVelocity
    {
        get
        {
            float max = float.MinValue;
            foreach (var part in _parts)
            {
                if (part.velocity.magnitude > max) max = part.velocity.magnitude;
            }

            return max;
        }
    }
    
    public float MinVelocity
    {
        get
        {
            float min = float.MaxValue;
            foreach (var part in _parts)
            {
                if (part.velocity.magnitude < min) min = part.velocity.magnitude;
            }

            return min;
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
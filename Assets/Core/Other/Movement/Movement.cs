using UnityEngine;

[RequireComponent(typeof(Mover))]
public abstract class Movement : MonoBehaviour
{
    protected Mover _mover;

    private void Awake()
    {
        EarlyInizialize();
    }

    private void Start()
    {
        LateInizialize();
    }

    protected virtual void EarlyInizialize()
    {
        _mover = GetComponent<Mover>();
        _mover.DestinationReached += OnDestinationReached;
    }

    protected virtual void LateInizialize()
    {

    }

    protected abstract void OnDestinationReached();
}
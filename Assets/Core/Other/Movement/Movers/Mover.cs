using UnityEngine;
using UnityEngine.Events;

public abstract class Mover : MonoBehaviour
{
    [SerializeField] private bool _stoppedByDefault;

    public event UnityAction DestinationReached;
    public event UnityAction MovementStopped;
    public event UnityAction MovementResumed;

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

    }
    
    protected virtual void LateInizialize()
    {
        if (_stoppedByDefault) Stop();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(ReachedDestination())
        {
            DestinationReached?.Invoke();
            return;
        }

        Move();
    }

    public virtual void Stop()
    {
        MovementStopped?.Invoke();
    }

    public virtual void Resume()
    {
        MovementResumed?.Invoke();
    }

    public abstract void SetDestination(Vector3 destination);
    protected abstract bool ReachedDestination();
    protected abstract void Move();
}
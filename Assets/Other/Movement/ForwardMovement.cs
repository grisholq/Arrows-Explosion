using UnityEngine;

public class ForwardMovement : Movement
{
    [SerializeField] private float _distance;

    protected override void LateInizialize()
    {
        base.LateInizialize();
        _mover.SetDestination(transform.position + transform.forward * _distance);
    }

    protected override void OnDestinationReached()
    {
        _mover.SetDestination(transform.position + transform.forward * _distance);
    }
}

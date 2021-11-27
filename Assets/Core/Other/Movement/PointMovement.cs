using UnityEngine;

public class PointMovement : Movement
{
    [SerializeField] private Transform _destinationTranform;

    private Vector3 _destination;

    protected override void LateInizialize()
    {
        base.LateInizialize();
        _destination = _destinationTranform.position;
        _mover.SetDestination(_destination);
    }

    protected override void OnDestinationReached()
    {
        _mover.Stop();
    }
}
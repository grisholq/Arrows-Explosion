using UnityEngine;
using System.Collections.Generic;

public class PatrolMovement : Movement
{
    [SerializeField] private Waypoints _waypoints;

    private IEnumerator<Vector3> _points;

    protected override void LateInizialize()
    {
        base.LateInizialize();
        _points = _waypoints.Points.GetEnumerator();
        _points.MoveNext();
        _mover.SetDestination(_points.Current);
    }

    protected override void OnDestinationReached()
    {
        if(_points.MoveNext() == false)
        {
            _points.Reset();
            _points.MoveNext();
        }

        _mover.SetDestination(_points.Current);
    }
}
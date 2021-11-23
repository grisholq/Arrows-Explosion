using UnityEngine;

public class TransformMover : Mover
{
    [SerializeField] private float _speed;
    [SerializeField] private float _reachDistance;

    private Vector3 _destination;
    private bool _stopped;

    public override void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    protected override void Move()
    {
        if (_stopped) return;
        transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * _speed);
    }

    protected override bool ReachedDestination()
    {
        return Vector3.Distance(transform.position, _destination) <= _reachDistance;
    }

    public override void Resume()
    {
        base.Resume();
        _stopped = false;
    }

    public override void Stop()
    {
        base.Stop();
        _stopped = true;
    }
}
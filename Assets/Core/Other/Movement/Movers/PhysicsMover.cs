using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMover : Mover
{
    [SerializeField] private float _speed;
    [SerializeField] private float _reachDistance;

    private Vector3 _destination;
    private Rigidbody _rigidbody;
    private bool _isStopped;

    protected override void EarlyInizialize()
    {
        base.EarlyInizialize();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    protected override void Move()
    {
        if (_isStopped) return;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    protected override bool ReachedDestination()
    {
        return Vector3.Distance(transform.position, _destination) <= _reachDistance;   
    }

    public override void Stop()
    {
        base.Stop();
        _isStopped = true;
    }

    public override void Resume()
    {
        base.Resume();
        _isStopped = false;
    }
}
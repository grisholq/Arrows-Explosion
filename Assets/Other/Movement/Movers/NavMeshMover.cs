using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : Mover
{
    [SerializeField] private NavMeshAgent _navAgent;

    protected override void LateInizialize()
    {
        base.LateInizialize();
        _navAgent.updateRotation = false;
    }

    public override void SetDestination(Vector3 destination)
    {
        _navAgent.SetDestination(destination);   
    }

    protected override void Move()
    {
        if (_navAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(_navAgent.velocity.normalized);
        }
    }

    protected override bool ReachedDestination()
    {
        return _navAgent.remainingDistance <= _navAgent.stoppingDistance;
    }

    public override void Resume()
    {
        base.Resume();
        _navAgent.isStopped = false;
    }

    public override void Stop()
    {
        base.Stop();
        _navAgent.isStopped = true;
    }
}
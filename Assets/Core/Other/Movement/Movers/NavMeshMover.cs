using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : Mover
{
    [SerializeField] private NavMeshAgent _navAgent;

    private bool _stopped;

    protected override void LateInizialize()
    {
        base.LateInizialize();
        _navAgent.updateRotation = false;
    }

    public override void SetDestination(Vector3 destination)
    {
        if (_stopped) return;
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
        if (_stopped) return false;

        return _navAgent.remainingDistance <= _navAgent.stoppingDistance;
    }

    public override void Resume()
    {
        base.Resume();
        _navAgent.isStopped = false;
        _navAgent.enabled = true;
        _stopped = false;
    }

    public override void Stop()
    {
        if (_stopped) return;

        base.Stop();
        _navAgent.isStopped = true;
        _navAgent.enabled = false;
        _stopped = true;
    }
}
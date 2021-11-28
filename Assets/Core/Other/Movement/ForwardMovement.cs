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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _distance);
        Gizmos.DrawSphere(transform.position + transform.forward * _distance, 0.2f);
    }
}

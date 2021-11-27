using UnityEngine;
using UnityEngine.Events;

public class RagdollVelocitiesChecker : MonoBehaviour
{
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private float _maxAverageVelocity;

    public event UnityAction ForceExeeded;

    private void FixedUpdate()
    {
        if(IsRagdoolAtRest() == false)
        {
            ForceExeeded?.Invoke();
        }
    }

    private bool IsRagdoolAtRest()
    {
        return _ragdoll.AverageVelocity <= _maxAverageVelocity;
    }
}
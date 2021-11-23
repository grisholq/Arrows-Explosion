using UnityEngine;
using UnityEngine.Events;

public class StickmanStability : MonoBehaviour
{
    [SerializeField] private RagdollVelocitiesChecker _ragdollVelocities;
    [SerializeField] private StickmanHitbox _stickmanHitbox;

    public event UnityAction Destabilized;

    private bool _destabilized = false;

    private void Awake()
    {
        _ragdollVelocities.ForceExeeded += Destabilize;
        _stickmanHitbox.Hitted += Destabilize;
    }

    public void Destabilize()
    {
        if (_destabilized == true) return;
        Destabilized?.Invoke();
    }
}
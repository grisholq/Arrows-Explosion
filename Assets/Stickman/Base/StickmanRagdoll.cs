using UnityEngine;

public class StickmanRagdoll : Ragdoll
{
    [SerializeField] private StickmanStability _stability;
        
    protected override void Inizialize()
    {
        base.Inizialize();
        _stability.Destabilized += Enable;
    }
}
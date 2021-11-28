using UnityEngine;

public class StickmanMover : NavMeshMover
{
    [SerializeField] private StickmanStability _stability;

    protected override void EarlyInizialize()
    {
        _stability.Destabilized += Stop;
    }
}
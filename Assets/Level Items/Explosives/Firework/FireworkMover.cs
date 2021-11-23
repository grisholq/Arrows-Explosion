using UnityEngine;

[RequireComponent(typeof(Firework))]
public class FireworkMover : PhysicsMover
{
    private Firework _firework;

    protected override void EarlyInizialize()
    {
        base.EarlyInizialize();
        _firework = GetComponent<Firework>();
        _firework.Launched += Resume;
    }
}
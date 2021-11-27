using UnityEngine;

[RequireComponent(typeof(Firework))]
public class FireworkShotObservable : ShotObservable
{
    private Firework _firework;

    protected override void Inizialize()
    {
        base.Inizialize();
        _firework = GetComponent<Firework>();
        _firework.Launched += AddAsShotObservable;
        _firework.Exploded += RemoveAsShotObservable;
    }

    private void Update()
    {
        CameraPosition = transform.position + new Vector3(0, 7, -10);
    }
}
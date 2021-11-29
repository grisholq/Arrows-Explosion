using UnityEngine;

[RequireComponent(typeof(Firework2))]
public class Firework2ShotObservable : ShotObservable
{
    private Firework2 _firework;

    protected override void Inizialize()
    {
        base.Inizialize();
        _firework = GetComponent<Firework2>();
        _firework.Launched += AddAsShotObservable;
        _firework.Exploded += RemoveAsShotObservable;
    }

    private void Update()
    {
        CameraPosition = transform.position + new Vector3(0, 7, -10);
    }
}

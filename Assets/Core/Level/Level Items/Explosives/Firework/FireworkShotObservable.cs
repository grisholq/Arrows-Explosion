using UnityEngine;

[RequireComponent(typeof(Firework))]
public class FireworkShotObservable : ShotObservable
{
    [SerializeField] private float _observationDelay;

    private Firework _firework;

    protected override void Inizialize()
    {
        base.Inizialize();
        _firework = GetComponent<Firework>();
        _firework.Launched += AddAsShotObservable;
        _firework.Exploded += StopObserving;
    }

    private void Update()
    {
        CameraPosition = transform.position + new Vector3(0, 7, -10);
    }

    private void StopObserving()
    {
        Timer delay = new Timer(_observationDelay);
        delay.Expired += RemoveAsShotObservable;
        delay.Launch();
    }
}
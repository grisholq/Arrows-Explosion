using UnityEngine;

[RequireComponent(typeof(Firework2))]
public class Firework2ShotObservable : ShotObservable
{
    [SerializeField] private float _observationDelay;

    private Firework2 _firework;

    protected override void Inizialize()
    {
        base.Inizialize();
        _firework = GetComponent<Firework2>();
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

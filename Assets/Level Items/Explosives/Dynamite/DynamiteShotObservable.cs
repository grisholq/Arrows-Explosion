using UnityEngine;

[RequireComponent(typeof(Dynamite))]
public class DynamiteShotObservable : ShotObservable
{
    [SerializeField] private float _observationDelay;

    private Dynamite _dynamite;

    protected override void Inizialize()
    {
        base.Inizialize();
        _dynamite = GetComponent<Dynamite>();
        _dynamite.Hit += AddAsShotObservable;
        _dynamite.Exploded += StopObserving;
    }

    private void Update()
    {
        CameraPosition = transform.position + new Vector3(0, 20, -20);
    }

    private void StopObserving()
    {
        Timer delay = new Timer(_observationDelay);
        delay.Expired += RemoveAsShotObservable;
        delay.Launch();
    }
}
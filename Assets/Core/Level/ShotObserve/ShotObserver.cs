using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ShotObserverObjects))]
public class ShotObserver : Singleton<ShotObserver>
{
    [SerializeField] private BallistaBoltShooter _shooter;
    [SerializeField] private UnityEvent _observationStarted;
    [SerializeField] private UnityEvent _observationEnded;

    public ShotObserverObjects ObserverObjects { get; protected set; }

    private bool _disabled;

    private void Awake()
    {
        _shooter.Shooted += StartObservation;

        ObserverObjects = new ShotObserverObjects();
        ObserverObjects.CurrentChanged += SetObservedObject;
        ObserverObjects.NoObjectsLeft += StopObservation;
    }

    public void StartObservation()
    {
        Sloumo.Instance.Activate();
        _observationStarted?.Invoke();     
    }

    public void StopObservation()
    {
        Sloumo.Instance.Deactivate();
        _observationEnded?.Invoke();
    }

    public void Disable()
    {
        Sloumo.Instance.Deactivate();
        _observationEnded?.Invoke();
        ObserverObjects.ClearShotObservables();
        _disabled = true;
    }

    public void AddObservedObject(ShotObservable observable)
    {
        if (_disabled) return;
        ObserverObjects.AddShotObservable(observable);
    }
    public void RemoveObservedObject(ShotObservable observable)
    {
        if (_disabled) return;
        ObserverObjects.RemoveShotObservable(observable);
    } 

    public void SetObservedObject(ShotObservable observable)
    {
        if (_disabled) return;
        observable.SetAsCameraObservable();
    }
}
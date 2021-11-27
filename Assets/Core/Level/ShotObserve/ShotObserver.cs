using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ShotObserverObjects))]
public class ShotObserver : Singleton<ShotObserver>
{
    [SerializeField] private Ballista _balista;
    [SerializeField] private UnityEvent _observationStarted;
    [SerializeField] private UnityEvent _observationEnded;

    public ShotObserverObjects ObserverObjects { get; protected set; }

    private void Awake()
    {      
        _balista.Shot += StartObservation;

        ObserverObjects = new ShotObserverObjects();
        ObserverObjects.CurrentChanged += SetObservedObject;
        ObserverObjects.NoObjectsLeft += StopObservation;
    }

    private void StartObservation()
    {
        Sloumo.Instance.Activate();
        _observationStarted?.Invoke();     
    }

    private void StopObservation()
    {
        Sloumo.Instance.Deactivate();
        _observationEnded?.Invoke();
    }

    public void AddObservedObject(ShotObservable observable) => ObserverObjects.AddShotObservable(observable);
    public void RemoveObservedObject(ShotObservable observable) => ObserverObjects.RemoveShotObservable(observable);

    public void SetObservedObject(ShotObservable observable)
    {
        observable.SetAsCameraObservable();
    }
}
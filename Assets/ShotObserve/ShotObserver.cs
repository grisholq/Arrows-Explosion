using UnityEngine;
using UnityEngine.Events;

public class ShotObserver : Singleton<ShotObserver>
{
    [SerializeField] private Ballista _balista;
    [SerializeField] private float _observationTime;

    [SerializeField] private UnityEvent _observationStarted;
    [SerializeField] private UnityEvent _observationEnded;

    private void Awake()
    {
        _balista.Shot += StartObservation;
        _balista.Reloaded += StopObservation;
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
}
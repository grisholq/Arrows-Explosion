using UnityEngine;
using UnityEngine.Events;

public class ShotObserver : Singleton<ShotObserver>
{
    [SerializeField] private Ballista _balista;
    [SerializeField] private UnityEvent _observationStarted;
    [SerializeField] private UnityEvent _observationEnded;

    private void Awake()
    {
        _balista.Shot += StartObservation;
    }

    private void StartObservation()
    {
        _observationStarted?.Invoke();     
    }

    private void StopObservation()
    {
        _observationEnded?.Invoke();
    }
}
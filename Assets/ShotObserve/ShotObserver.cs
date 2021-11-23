using UnityEngine;
using UnityEngine.Events;

public class ShotObserver : MonoBehaviour
{
    [SerializeField] private Ballista _balista;
    [SerializeField] private float _observationTime;

    [SerializeField] private UnityEvent _observationStarted;
    [SerializeField] private UnityEvent _observationEnded;

    private void Awake()
    {
        _balista.Shot += StartObservation;
    }

    private void StartObservation()
    {
        _observationStarted?.Invoke();
        Sloumo.Instance.Activate();

        Timer period = new Timer(_observationTime);
        period.Expired += StopObservation;
        period.Launch();
    }

    private void StopObservation()
    {
        _observationStarted?.Invoke();
    }
}
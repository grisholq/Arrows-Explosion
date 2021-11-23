using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BallistaTraectoryCalculator))]
public class Ballista : MonoBehaviour
{
    [SerializeField] private float _reloadTime;
    [SerializeField] private BoltsBarrageInstantiator _barrageInstantiator;
    [SerializeField] private BallistaTraectoryCalculator _traectoryCalculator;
    [SerializeField] private GameObject _shotInput;

    public event UnityAction Shot;

    private ISlideInput _input; 
    private bool _loaded = true;

    private void Awake()
    {
        _input = _shotInput.GetComponent<ISlideInput>();
        _input.Ended += Shoot;
    }

    public void Shoot()
    {
        if (_loaded == false) return;

        BoltsBarrage barrage = _barrageInstantiator.GetBoltsBarrage();
        barrage.Launch(_traectoryCalculator.Current);
        Shot?.Invoke();

        StartReload();
    }

    private void StartReload()
    {
        _loaded = false;

        Timer timer = new Timer(_reloadTime);
        timer.Expired += (() => _loaded = true);
        timer.Launch();
    }
}
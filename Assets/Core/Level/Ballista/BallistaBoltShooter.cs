using UnityEngine;
using UnityEngine.Events;

public class BallistaBoltShooter : MonoBehaviour
{
    [SerializeField] private float _reloadTime;
    [SerializeField] private BallistaAmmo _ammo;
    [SerializeField] private BoltsBarrageInstantiator _barrages;
    [SerializeField] private BallistaTraectoryCalculator _traectory;
    [SerializeField] private GameObject _inputGO;

    public event UnityAction Shooted;

    private ISlideInput _input;

    private void Awake()
    {
        _input = _inputGO.GetComponent<ISlideInput>();
        _input.Ended += Shoot;
    }

    public void Shoot()
    {
        
        BoltsBarrage barrage = _barrages.GetBoltsBarrage();
        barrage.Launch(_traectory.Current);
        _ammo.SpendAmmo();
        Shooted?.Invoke();
        _input.Disable();

        Reload();
    }

    private void Reload()
    {   
        if (_ammo.OutOfAmmo) return;

        Timer timer = new Timer(_reloadTime);
        timer.Expired += _input.Enable;
        timer.Launch();
    }
}
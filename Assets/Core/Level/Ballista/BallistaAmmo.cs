using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Ballista))]
public class BallistaAmmo : MonoBehaviour
{
    [SerializeField] private int _ammoAmount;

    public event UnityAction OutOfAmmo;

    private Ballista _ballista;
    private int _currentAmmo;

    private void Awake()
    {
        _ballista = GetComponent<Ballista>();
        _ballista.Shot += SpendAmmo;

        _currentAmmo = _ammoAmount;
    }

    public void SpendAmmo()
    {
        _currentAmmo--;

        if(_currentAmmo < 0)
        {
            OutOfAmmo?.Invoke();
        }
    }
}
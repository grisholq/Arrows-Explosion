using UnityEngine;
using UnityEngine.Events;

public class BallistaAmmo : MonoBehaviour
{
    [SerializeField] private UnityEvent<int, int> _ammoChanged;
    [SerializeField] private int _ammoAmount;

    public bool OutOfAmmo => CurrentAmmo <= 0;

    private int CurrentAmmo
    {
        get => _currentAmmo;

        set
        {
            _currentAmmo = value;
            _ammoChanged.Invoke(_currentAmmo, _ammoAmount);
        }
    }

    private int _currentAmmo;

    private void Awake()
    {
        CurrentAmmo = _ammoAmount;
    }

    public void SpendAmmo()
    {
        CurrentAmmo--;
    }
}
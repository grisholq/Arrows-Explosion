using UnityEngine;
using UnityEngine.Events;

public class BallistaAmmo : MonoBehaviour
{
    [SerializeField] private UnityEvent _outOfAmmo;
    [SerializeField] private UnityEvent<int> _ammoChanged;
    [SerializeField] private int _ammoAmount;

    public bool OutOfAmmo => CurrentAmmo <= 0;

    private int CurrentAmmo
    {
        get => _currentAmmo;

        set
        {
            _currentAmmo = value;
            _ammoChanged.Invoke(_currentAmmo);
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
    
    public void CheckAmmo()
    {
        if(CurrentAmmo == 0)
        {
            _outOfAmmo?.Invoke();
        }
    }
}
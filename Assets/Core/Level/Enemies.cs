using UnityEngine;
using UnityEngine.Events;

public class Enemies : MonoBehaviour
{
    [SerializeField] private UnityEvent _allEnemiesDied;
 
    public int CurrentAmount
    {
        get => _currentAmount;

        private set
        {
            _currentAmount = Mathf.Max(0, value);
            if (_currentAmount <= 0) _allEnemiesDied?.Invoke();
        }
    }

    private int _currentAmount;

    private void Awake()
    {
        Stickman[] stickmen = GetComponentsInChildren<Stickman>();

        foreach (var stickman in stickmen)
        {
            stickman.Died += () => CurrentAmount--;
        }

        CurrentAmount = transform.childCount;
    }
}
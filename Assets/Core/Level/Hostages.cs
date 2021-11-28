using UnityEngine;
using UnityEngine.Events;

public class Hostages : MonoBehaviour
{
    [SerializeField] private UnityEvent _hostagesDied;
    [SerializeField] private UnityEvent<int, int> _amountChanged;
    [SerializeField] private int _winAmount;

    public int CurrentAmount
    {
        get => _currentAmount;

        private set
        {
            _currentAmount = Mathf.Max(0, value);
            if (_currentAmount < _winAmount) _hostagesDied?.Invoke();
            _amountChanged?.Invoke(_currentAmount, _winAmount);
        }
    }

    private StickmanAnimatorInterface[] _stickmanAnimations;
    private StickmanEmotions[] _stickmanEmotions;

    private int _currentAmount;

    private void Awake()
    {
        _stickmanAnimations = GetComponentsInChildren<StickmanAnimatorInterface>();
        _stickmanEmotions = GetComponentsInChildren<StickmanEmotions>();

        Stickman[] stickmen = GetComponentsInChildren<Stickman>();

        foreach (var stickman in stickmen)
        {
            stickman.Died += () => CurrentAmount--;
        }

        CurrentAmount = transform.childCount;
    }

    public void ShowEmotions()
    {
        foreach (var stickman in _stickmanEmotions)
        {
            stickman.ShowEmojy();
        }
    }
    
    public void Dance()
    {
        foreach (var stickman in _stickmanAnimations)
        {
            stickman.Dance();
        }
    }
}
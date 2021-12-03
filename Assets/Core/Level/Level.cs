using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private float _pauseBeforeWin;
    [SerializeField] private UnityEvent _victory;
    [SerializeField] private UnityEvent _victoryDelay;
    [SerializeField] private UnityEvent _friendsLose;
    [SerializeField] private UnityEvent _ammoLose;
    [SerializeField] private UnityEvent _levelEnd;

    private bool _levelEnded = false;

    public void Win()
    {
        if (_levelEnded) return;
        _levelEnded = true;
        _levelEnd.Invoke();

        _victory?.Invoke();

        Timer delay = new Timer(_pauseBeforeWin);
        delay.Expired += _victoryDelay.Invoke;
        delay.Launch();
    }

    public void LoseFriends()
    {
        if (_levelEnded == true) return;
        _levelEnded = true;

        _friendsLose.Invoke();
        _levelEnd.Invoke();
    }

    public void LoseAmmo()
    {
        if (_levelEnded == true) return;
        _levelEnded = true;

        _ammoLose.Invoke();
        _levelEnd.Invoke();
    }
}
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private float _pauseBeforeEndGame;
    [SerializeField] private UnityEvent _victory;
    [SerializeField] private UnityEvent _friendsLose;
    [SerializeField] private UnityEvent _ammoLose;
    [SerializeField] private UnityEvent _levelEnd;

    private bool _levelEnded = false;

    public void Win()
    {
        if (_levelEnded) return;
        _levelEnded = true;

        _victory.Invoke();
        _levelEnd.Invoke();
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

    private void EndGame(UnityAction action, float pause)
    {
        if (_levelEnded == true) return;

        Timer timer = new Timer(pause);
        timer.Expired += action.Invoke;
        timer.Launch();
    }
}
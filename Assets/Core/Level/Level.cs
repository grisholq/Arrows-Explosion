using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private UnityEvent _victory;
    [SerializeField] private UnityEvent _friendsLose;
    [SerializeField] private UnityEvent _ammoLose;
    [SerializeField] private UnityEvent _levelEnd;

    private bool _levelEnded = false;

    public void Win()
    {
        if (_levelEnded == true) return;
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
}
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private UnityEvent _victory;
    [SerializeField] private UnityEvent _friendsLose;
    [SerializeField] private UnityEvent _ammoLose;

    public void Win()
    {
        _victory.Invoke();
    }

    public void LoseFriends()
    {
        _friendsLose.Invoke();
    }

    public void LoseAmmo()
    {
        _ammoLose.Invoke();
    }
}
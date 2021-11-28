using UnityEngine;
using UnityEngine.Events;

public class Hostages : MonoBehaviour
{
    [SerializeField] private UnityEvent _hostagesDied;
    [SerializeField] private int _loseAmount;

    private int _currentAmount;

    private void Update()
    {
        
    }
}
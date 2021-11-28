using UnityEngine;
using UnityEngine.Events;

public class Stickman : MonoBehaviour
{
    [SerializeField] private StickmanStability _stability;

    public event UnityAction Died;

    private bool _died;

    private void Awake()
    {
        _stability.Destabilized += Die;
    }

    public void Die()
    {
        if (_died) return;
        Died?.Invoke();
        _died = true;
    }
}
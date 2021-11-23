using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour, IDamagable
{
    [SerializeField] private bool _disableOnHit;

    public event UnityAction Hitted;

    private bool _disabled;

    public virtual void Hit()
    {
        if (_disabled) return;
        Hitted?.Invoke();
        _disabled = true;
    }

    public void RecieveDamage(float damage)
    {
        Hit();
    }
}
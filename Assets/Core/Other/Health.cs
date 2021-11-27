using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;
    [SerializeField] private float _max;
    
    public event UnityAction Died;

    public float Value 
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0, _max);
            if (_value <= 0) Die();
        }
    }

    public void Heal(float heal)
    {
        Value += heal;
    }
    
    public void HealToMax()
    {
        Value = _max;
    }

    public void Damage(float damage)
    {
        Value -= damage;
    }

    public void Die()
    {
        if (Died != null) Died.Invoke();
    }
}
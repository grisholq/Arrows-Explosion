using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class DestructableItemPart : MonoBehaviour, IDamagable
{
    public event UnityAction<float> Damaged;
    public event UnityAction Hit;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void RecieveDamage(float damage = 0)
    {
        Damaged?.Invoke(damage);
        Hit?.Invoke();
    }

    public void Break()
    {
        _rigidbody.isKinematic = false;
    }
}
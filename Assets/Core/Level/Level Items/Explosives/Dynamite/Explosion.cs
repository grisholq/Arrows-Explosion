using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Explosion : MonoBehaviour, IDamager
{
    [SerializeField] private float _delay;
    [SerializeField] private float _explosionRange;
    [SerializeField] private float _explosionForce;
    [SerializeField] private Vector3 _explosionOffset;
    [SerializeField] private float _damage;

    public event UnityAction Exploded;

    public void Explode()
    {
        DamageObjectsInRange();
        ForceObjectsInRange();
    }

    private void DamageObjectsInRange()
    {
        List<IDamagable> damagables = GetIDamagablesInExplosionRange();
        DamageIDamagables(damagables);
    }

    private void ForceObjectsInRange()
    {
        List<Rigidbody> rigidbodies = GetRigidbodiesInExplosionRange();
        AddExplosionForceToRigidbodies(rigidbodies);
    }

    private List<Rigidbody> GetRigidbodiesInExplosionRange()
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position + _explosionOffset, _explosionRange);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbodies.Add(rigidbody);
            }
        }

        return rigidbodies;
    }

    private List<IDamagable> GetIDamagablesInExplosionRange()
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position + _explosionOffset, _explosionRange);

        List<IDamagable> damagables = new List<IDamagable>();

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamagable damagable))
            {
                damagables.Add(damagable);
            }
        }

        return damagables;
    }

    public void DamageIDamagables(List<IDamagable> damagables)
    {
        foreach (var damagable in damagables)
        {
            Damage(damagable);
        }
    }
    
    public void AddExplosionForceToRigidbodies(List<Rigidbody> rigidbodies)
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRange);
        }
    }

    public void Damage(IDamagable damagable)
    {
        damagable.RecieveDamage(_damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + _explosionOffset, _explosionRange);
        Gizmos.DrawSphere(transform.position + _explosionOffset, 0.1f);
    }
}
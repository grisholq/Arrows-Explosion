using UnityEngine;
using DCFAEngine;

public class Bolt : MonoBehaviour, IDamager
{
    [SerializeField] private float _force;
    [SerializeField] private float _damage;
    [SerializeField] private float _hitDistance;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private GameSound _hitSound;

    private bool _stuck = false;

    private void Awake()
    {
        transform.eulerAngles = Vector3.forward * Random.Range(0f, 360f);
    }

    private void Update()
    {
        if (_stuck) return;

        if(TryHit(out Transform hitTransform))
        {
            ProcessHit(hitTransform);
        }
    }

    private bool TryHit(out Transform hitTransform)
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _hitDistance))
        {
            hitTransform = hit.transform;
            return true;
        }

        hitTransform = null;
        return false;
    }
        
    private void ProcessHit(Transform hitTransform)
    {
        _stuck = true;
        transform.SetParent(hitTransform, true);
        _trailRenderer.emitting = false;
        _hitSound.Play(GetComponent<AudioSource>());

        if (hitTransform.TryGetComponent(out IDamagable damagable))
        {
            Damage(damagable);
        }
        
        if (hitTransform.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddForce(transform.forward * _force);
        }
    }

    public void Damage(IDamagable damagable)
    {
        damagable.RecieveDamage(_damage);
    }
}
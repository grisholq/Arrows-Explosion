using UnityEngine;
using DCFAEngine;

public class Bolt : MonoBehaviour, IDamager
{
    [SerializeField] private float _force;
    [SerializeField] private float _damage;
    [SerializeField] private float __additionalDistance = 1.2f; 
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private GameSound _hitSound;

    private bool _stuck = false;

    private void Awake()
    {
        lastPosition = transform.position;
        transform.eulerAngles = Vector3.forward * Random.Range(0f, 360f);
    }

    private void LateUpdate()
    {
        if (_stuck) return;

        if(TryHit(out Transform hitTransform))
        {
            ProcessHit(hitTransform);
        }
    }

    private Vector3 lastPosition = Vector3.zero;
    private bool TryHit(out Transform hitTransform)
    {
        Vector3 direction = Vector3.Normalize(transform.position - lastPosition);
        float hitDistance = Vector3.Distance(transform.position, lastPosition) + __additionalDistance;

        Debug.DrawRay(lastPosition, direction, Color.red, hitDistance);

        lastPosition = transform.position;
        if (Physics.Raycast(lastPosition, direction, out RaycastHit hit, hitDistance))
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * -__additionalDistance);
    }
}
using DCFAEngine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Explosion))]
public class Firework : MonoBehaviour, IDamagable, IDamager
{
    [SerializeField] private float _damageToExplodeInstantly;
    [SerializeField] private float _explosionTime;
    [SerializeField] private SpecialEffect _explosionPrefab;

    public event UnityAction Launched;
    public event UnityAction Exploded;

    private Explosion _explosion;
    private bool _launched = false;
    private bool _exploded = false;

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
    }

    public void Launch()
    {
        if (_launched) return;

        Launched?.Invoke();
        ExplodeIn(_explosionTime);
        _launched = true;
    }

    private void ExplodeIn(float time)
    {
        Timer explosionTimer = new Timer(time);
        explosionTimer.Expired += Explode;
        explosionTimer.Launch();
    }

    private void Explode()
    {
        if (_exploded == true) return;

        Exploded?.Invoke();
        _exploded = true;
        _explosion.Explode();
        SpecialEffect explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.Show();
        Destroy(gameObject);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_launched == false) return;

        if (other.TryGetComponent(out IDamagable damagable))
        {
            Damage(damagable);
        }

        Explode();
    }

    public void RecieveDamage(float damage = 0)
    {
        if(damage < _damageToExplodeInstantly)
        {
            Launch();
        }
        else
        {
            Timer delay = new Timer(0.1f);
            delay.Expired += Explode;
            delay.Launch();
        }      
    }

    public void Damage(IDamagable damagable)
    {
        damagable.RecieveDamage();
    }
}
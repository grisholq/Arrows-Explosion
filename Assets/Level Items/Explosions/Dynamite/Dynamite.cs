using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class Dynamite : MonoBehaviour, IDamagable
{
    [SerializeField] private ParticleSystem _explosionParticles;

    private Explosion _explosion;
    private bool _exploded;

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
    }

    public void RecieveDamage(float damage = 0)
    {
        Explode();
    }

    public void Explode()
    {
        if (_exploded == true) return;

        _exploded = true;
        _explosion.Explode();
        Instantiate(_explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);       
    }
}
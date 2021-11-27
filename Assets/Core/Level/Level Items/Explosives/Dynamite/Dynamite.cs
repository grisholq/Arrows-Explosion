using DCFAEngine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Explosion))]
public class Dynamite : MonoBehaviour, IDamagable
{
    [SerializeField] private SpecialEffect _explosionPrefab;

    public event UnityAction Hit;
    public event UnityAction Exploded;

    private Explosion _explosion;
    private bool _hit;

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
    }

    public void RecieveDamage(float damage = 0)
    {
        if (_hit) return;

        Hit?.Invoke();
        _hit = true;
        Timer delay = new Timer(0.1f);
        delay.Expired += Explode;
        delay.Launch();
    }

    public void Explode()
    {
        Exploded?.Invoke();
        _explosion.Explode();
        SpecialEffect explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.Show();
        gameObject.SetActive(false);
        Destroy(gameObject, 0.3f);       
    }
}
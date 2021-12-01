using DCFAEngine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DestructableItemHealth))]
public class DestructableItem : MonoBehaviour, IDamagable
{
    [SerializeField] private UnityEvent Broke;

    public event UnityAction<float> Damaged;
   
    private DestructableItemPart[] _parts;
    private DestructableItemHealth _health;

    private bool _broken = false;
    [SerializeField]
    private GameSound destroySound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _parts = GetComponentsInChildren<DestructableItemPart>();
        _health = GetComponent<DestructableItemHealth>();

        _health.Died += Break;

        foreach (var part in _parts)
        {
            part.Damaged += RecieveDamage;
        }
    }

    public void RecieveDamage(float damage = 0)
    {
        Damaged?.Invoke(damage);
    }

    public void Break()
    {
        if (_broken) return;

        foreach (var part in _parts)
        {
            part.Break();
        }

        Broke?.Invoke();
        _broken = true;

        if(audioSource != null && destroySound != null)
            destroySound?.Play(audioSource);
    }
}
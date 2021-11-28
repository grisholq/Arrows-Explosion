using UnityEngine;

public class CombinedHitbox : Hitbox
{
    private Hitbox[] _hitboxes;

    private void Awake()
    {
        _hitboxes = GetComponentsInChildren<Hitbox>();

        foreach (var hitbox in _hitboxes)
        {
            hitbox.Hitted += Hit;
        }

        this.Hitted -= Hit;
    }
}
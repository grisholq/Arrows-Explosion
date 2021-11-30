using UnityEngine;

public class FireworkExplosionSound : Sound
{
    [SerializeField] private Firework2 _firework;

    private void Awake()
    {
        _firework.Exploded += Play;
    }
}
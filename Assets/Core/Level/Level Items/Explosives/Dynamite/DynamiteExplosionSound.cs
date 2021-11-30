using UnityEngine;

public class DynamiteExplosionSound : Sound
{
    [SerializeField] private Dynamite _dynamite;

    private void Awake()
    {
        _dynamite.Exploded += Play;
    }
}
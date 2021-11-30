using UnityEngine;

public class FireworkFlightSound : Sound
{
    [SerializeField] private Firework2 _firework;

    private void Awake()
    {
        _firework.Launched += Play;
    }
}
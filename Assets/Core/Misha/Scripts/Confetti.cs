using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    private ParticleStarter particleStarter;
    private static readonly List<Confetti> cache = new List<Confetti>();
    public static IList<Confetti> GetAllConfetti() => cache;

    private void Awake()
    {
        particleStarter = GetComponent<ParticleStarter>();
        cache.Add(this);
    }

    private void OnDestroy()
    {
        cache.Remove(this);
    }

    public void Play()
    {
        particleStarter.Play("First");
    }
    public void Stop()
    {
        particleStarter.Stop("First");
    }
}

using UnityEngine;
using DCFAEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfettiManager : Singleton<ConfettiManager>
{
    [SerializeField]
    private GameRegion2D region;
    [SerializeField]
    private Firework2 fireworkPrefab;
    [SerializeField]
    private RangedFloat fireworkCooldown;
    [SerializeField]
    private RangedFloat ditance;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float soundDelay = 1f;
    public void PlayAllConfetti()
    {
        if (isPlayed)
            return;
        StartCoroutine(PlayVinSound());
        spawnerCoroutine = StartCoroutine(SpawnFireworks());
        isPlayed = true;
        foreach (var confetti in Confetti.GetAllConfetti())
        {
            confetti.Play();
        }
    }

    private void StopAllConfetti()
    {
        if (!isPlayed)
            return;
        if (spawnerCoroutine != null)
            StopCoroutine(spawnerCoroutine);
        isPlayed = false;
        foreach (var confetti in Confetti.GetAllConfetti())
        {
            confetti.Stop();
        }
    }

    private IEnumerator PlayVinSound()
    {
        yield return new WaitForSeconds(soundDelay);
        audioSource.Play();

    }

    private Coroutine spawnerCoroutine;
    private IEnumerator SpawnFireworks()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireworkCooldown.GetRandom());
            Vector3 pos = region.GetRandomPoint();
            Firework2 firework = Instantiate(fireworkPrefab, pos, fireworkPrefab.transform.rotation);
            firework.MaxDistance = ditance.GetRandom();
            ((IDamagable)firework).RecieveDamage(999f);
        }
    }

    private bool isPlayed = false;
}

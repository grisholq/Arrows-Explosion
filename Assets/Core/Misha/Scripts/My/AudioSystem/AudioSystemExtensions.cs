using UnityEngine;

namespace DCFAEngine
{
    public static class AudioSystemExtensions
    {
        public static void PlaySound(this AudioSource audioSource, GameSound audioPrefab)
        {
            if (audioSource == null || audioPrefab == null)
                return;
            audioPrefab.Play(audioSource);
        }

        public static void PlaySound(this AudioSource audioSource, GameSound audioPrefab, float pithPercent)
        {
            if (audioSource == null || audioPrefab == null)
                return;
            audioPrefab.Play(audioSource, pithPercent);
        }
    }
}

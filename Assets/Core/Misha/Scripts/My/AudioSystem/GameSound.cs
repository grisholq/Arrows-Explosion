using UnityEngine;

namespace DCFAEngine
{
    [CreateAssetMenu(fileName = "GameSound", menuName = "Data/GameSound", order = 1)]
    public class GameSound : ScriptableObject
    {
        public AudioClip[] clipVariants;
        public RangedFloat pith = new RangedFloat(0.9f, 0.2f);

        public AudioClip GetRandomClip()
        {
            if (clipVariants == null || clipVariants.Length == 0)
                return null;

            return clipVariants[Random.Range(0, clipVariants.Length)];
        }

        [SerializeField]
        private float cooldown = 0.3f;
        public float Cooldown => cooldown;

        public bool IsNoCooldown => cooldown <= 0;

        public void Play(AudioSource audioSource)
        {
            AudioSystem.Instance.PlayOneShot(this, audioSource);
        }

        public void Play(AudioSource audioSource, float pithPercent)
        {
            AudioSystem.Instance.PlayOneShot(this, audioSource);
        }
    }
}

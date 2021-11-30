using System;
using UnityEngine;

namespace DCFAEngine
{
    [Serializable]
    public class GameSoundSession
    {
        private GameSound sound;
        public GameSound Sound => sound;

        private float cooldownTime = 0;

        public GameSoundSession(GameSound sound)
        {
            this.sound = sound;
        }

        public bool CooldownRunned => cooldownTime > 0;

        public int TicksInFrame
        {
            get
            {
                if (CooldownRunned)
                    return 0;

                return -Mathf.FloorToInt(cooldownTime / sound.Cooldown);
            }
        }

        public void PlaySound(AudioSource source)
        {
            if (CooldownRunned && !sound.IsNoCooldown)
                return;

            source.pitch = sound.pith.GetRandom();
            source.PlayOneShot(sound.GetRandomClip());

            if (!sound.IsNoCooldown)
                cooldownTime += sound.Cooldown;
        }

        public void Update(float dt)
        {
            if (cooldownTime > 0)
                cooldownTime -= dt;
        }
    }
}

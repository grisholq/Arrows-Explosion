using DCFAEngine.Singletons;
using System.Collections.Generic;
using UnityEngine;

namespace DCFAEngine
{
    public class AudioSystem : ScSingleton<AudioSystem>
    {
        private readonly SerializableDictionary<GameSound, GameSoundSession> soundSessions = new SerializableDictionary<GameSound, GameSoundSession>();

        private void Awake()
        {
            DCFAEngineLoop.Instance.OnUpdate += OnEndOfFrame;
        }

        private void OnEndOfFrame(float deltaTime)
        {
            foreach (var item in soundSessions)
            {
                item.Value.Update(Time.deltaTime);
            }
        }

        public void ClearBuffer()
        {
            soundSessions.Clear();
        }

        public void PlayOneShot(GameSound sound, AudioSource source)
        {
            if (!soundSessions.ContainsKey(sound))
                soundSessions.Add(sound, new GameSoundSession(sound));

            soundSessions[sound].PlaySound(source);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DCFAEngine;

namespace DCFAEngine
{
    [RequireComponent(typeof(AudioSource))]
    public class AnimationEventSounds : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private AnimationEventTranslator eventTranslator;

        public SerializableDictionary<string, GameSound> eventClipPairs;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (eventTranslator == null)
            {
                Debug.LogWarning("Отсутсвует транслятор событий");
                return;
            }

            eventTranslator.OnEventCalled += EventTranslatorOnGetEvent;
        }

        private void EventTranslatorOnGetEvent(string eventName)
        {
            if (eventClipPairs.TryGetValue(eventName, out GameSound gameSound))
            {
                gameSound.Play(audioSource);
            }
        }
    }
}

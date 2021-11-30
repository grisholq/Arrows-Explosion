using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DCFAEngine;

namespace DCFAEngine
{
    [RequireComponent(typeof(AnimationEventTranslator))]
    public class AnimationEventVibration : MonoBehaviour
    {
        [System.Serializable]
        public class VibrationClip
        {
            public long timeMillisecond;
            public VibrationClip(long timeMillisecond)
            {
                this.timeMillisecond = timeMillisecond;
            }
        }

        private AnimationEventTranslator eventTranslator;
        public SerializableDictionary<string, VibrationClip> eventClipPairs;

        private void Awake()
        {
            eventTranslator = GetComponent<AnimationEventTranslator>();
            eventTranslator.OnEventCalled += EventTranslatorOnGetEvent;
        }

        private void EventTranslatorOnGetEvent(string eventName)
        {
            if (eventClipPairs.TryGetValue(eventName, out VibrationClip clip))
            {
                VibrationController.Instance.VibrateM(clip.timeMillisecond);
            }
        }
    }
}

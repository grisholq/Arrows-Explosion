using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCFAEngine
{
    public class AnimationEventTranslator : MonoBehaviour
    {
        public bool enable = true;

        private readonly Dictionary<string, List<Action>> listeners = new Dictionary<string, List<Action>>();

        public void CallEvent(string eventName)
        {
            if (!enable)
                return;

            OnEventCalled?.Invoke(eventName);

            if (!listeners.ContainsKey(eventName))
                return;
            foreach (var listener in listeners[eventName])
            {
                listener?.Invoke();
            }
        }

        public void AddListener(string eventName, Action listener)
        {
            if (listeners.ContainsKey(eventName) == false)
            {
                listeners.Add(eventName, new List<Action>());
            }
            listeners[eventName].Add(listener);
        }

        public void RemoveListener(string animationname, Action listener)
        {
            if (listeners.ContainsKey(animationname) == false)
                return;

            if (listeners[animationname].Contains(listener) == false)
                return;

            listeners[animationname].Remove(listener);
        }

        public delegate void OnGetEventHandler(string eventName);
        public event OnGetEventHandler OnEventCalled;

        public static SortedEvent<string, GameObject> OnGlobalGetEvent;
    }


}
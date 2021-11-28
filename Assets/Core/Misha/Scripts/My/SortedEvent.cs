using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace DCFAEngine
{
    public class SortedEvent<Trg, Arg>
    {
        public delegate void SortedEventHandler(Trg trigger, Arg arg);
        private Dictionary<Trg, SortedEventHandler> listeners = new Dictionary<Trg, SortedEventHandler>();

        public void AddListener(Trg trigger, SortedEventHandler listener)
        {
            if (!listeners.ContainsKey(trigger))
                listeners.Add(trigger, listener);
            else
                listeners[trigger] += listener;
        }
        public void RemoveListener(Trg trigger, SortedEventHandler listener)
        {
            if (!listeners.ContainsKey(trigger))
                return;

            listeners[trigger] -= listener;
        }

        public int GetListenersCount(Trg trigger)
        {
            if (!listeners.ContainsKey(trigger))
                return 0;

            return listeners[trigger].GetInvocationList().Length;
        }

        public void Invoke(Trg trigger, Arg arg)
        {
            if (GetListenersCount(trigger) <= 0)
                return;

            listeners[trigger].Invoke(trigger, arg);
        }
    }


    public class SortedEvent<Trg>
    {
        public delegate void SortedEventHandler(Trg trigger);
        private Dictionary<Trg, SortedEventHandler> listeners = new Dictionary<Trg, SortedEventHandler>();

        public void AddListener(Trg trigger, SortedEventHandler listener)
        {
            if (!listeners.ContainsKey(trigger))
                listeners.Add(trigger, listener);
            else
                listeners[trigger] += listener;
        }
        public void RemoveListener(Trg trigger, SortedEventHandler listener)
        {
            if (!listeners.ContainsKey(trigger))
                return;

            listeners[trigger] -= listener;
        }

        public int GetListenersCount(Trg trigger)
        {
            if (!listeners.ContainsKey(trigger))
                return 0;
            if (listeners[trigger] == null)
            {
                listeners.Remove(trigger);
                return 0;
            }
            return listeners[trigger].GetInvocationList().Length;
        }

        public void Invoke(Trg trigger)
        {
            if (GetListenersCount(trigger) <= 0)
                return;

            listeners[trigger].Invoke(trigger);
        }
    }
}

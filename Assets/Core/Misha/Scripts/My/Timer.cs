using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DCFAEngine
{
    [Serializable]
    public class Timer
    {
        [SerializeField]
        protected float interval;
        public float Interval { get => interval; }

        [SerializeField]
        protected float time;
        public float Time { get => Time; }

        public bool isRunned { get; private set; } = false;
        
        public static void StartAnonimTimer(float interval, Action<Timer, StopResone> StopedCallBack = null)
        {
            new Timer().Start(interval, StopedCallBack);
        }

        public Timer() { }
        public Timer(float interval) { this.interval = interval; }


        private float speed = 1f;
        public float Speed
        { 
            get => speed; 
            set => speed = value; 
        }

        public void Start(float interval, Action<Timer, StopResone> StopedCallBack = null)
        {
            this.interval = interval;
            Start(StopedCallBack);
        }
        public void Start(Action<Timer, StopResone> StopedCallBack = null)
        {
            DCFAEngineLoop.Instance.OnPreUpdate += Update;

            if (StopedCallBack != null)
                OnStopedCallBack = StopedCallBack;

            if (isRunned)
                Stop();

            time = interval;
            isRunned = true;

            OnStart?.Invoke(this);

            if (time <= 0)
            {
                DoElapsed();
            }
        }

        private void Update(float deltaTime)
        {
            if (time <= 0)
                return;

            time -= deltaTime * speed;
            if (time <= 0)
            {
                DoElapsed();
            }
        }

        public void Stop()
        {
            Stop(StopResone.Stoped);
        }

        private void Stop(StopResone resone)
        {
            DCFAEngineLoop.Instance.OnPreUpdate -= Update;
            time = 0;
            isRunned = false;
            OnStoped?.Invoke(this, resone);
            OnStopedCallBack?.Invoke(this, resone);
        }

        protected void DoElapsed()
        {
            Stop(StopResone.Elapsed);
            OnElapsed?.Invoke(this);
        }

        public float GetTimePercent()
        {
            return time / interval;
        }

        public bool IsPaused { get; private set; } = false;
        public void Pause()
        {
            if (IsPaused)
                return;
            IsPaused = true;
            DCFAEngineLoop.Instance.OnPreUpdate -= Update;
        }

        public void UnPause()
        {
            if (!IsPaused)
                return;
            IsPaused = false;
            DCFAEngineLoop.Instance.OnPreUpdate += Update;
        }

        public event Action<Timer> OnStart;
        public event Action<Timer> OnElapsed;
        public event Action<Timer, StopResone> OnStoped;
        private Action<Timer, StopResone> OnStopedCallBack;

        public enum StopResone
        {
            Elapsed,
            Stoped,
        }
    }
}


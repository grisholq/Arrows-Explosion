using DCFAEngine.Singletons;
using System.Collections;
using UnityEngine;

namespace DCFAEngine
{
    public class DCFAEngineLoop : ScSingleton<DCFAEngineLoop>
    {
        private Coroutine additionalLoop;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        private void Awake()
        {
            OnInitialized?.Invoke();
            additionalLoop = StartCoroutine(AdditionalLoop());
        }

        private void Update()
        {
            OnPreUpdate?.Invoke(Time.deltaTime);
            OnUpdate?.Invoke(Time.deltaTime);
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke(Time.deltaTime);
        }

        private IEnumerator AdditionalLoop()
        {
            while (true)
            {
                yield return waitForEndOfFrame;
                OnEndOfFrame?.Invoke(Time.deltaTime);
            }
        }

        public event System.Action OnInitialized;

        public delegate void UpdateHandler(float deltaTime);
        public event UpdateHandler OnPreUpdate;
        public event UpdateHandler OnUpdate;
        public event UpdateHandler OnLateUpdate;
        public event UpdateHandler OnFixedUpdate;
        public event UpdateHandler OnEndOfFrame;
    }
}

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DCFAEngine
{
    public class FrameRateController : MonoBehaviour
    {
        [SerializeField]
        private int frameRate = 60;

        private void Start()
        {
            Apply();
        }


        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif

            Apply();
        }

        private void Apply() => Application.targetFrameRate = frameRate;

        public void SetFrameRate(int value)
        {
            frameRate = value;
            Apply();
        }

        private void OnDestroy()
        {
            Application.targetFrameRate = -1;
        }
    }
}
using DCFAEngine.Singletons;
using UnityEngine;

namespace DCFAEngine
{
    public class VibrationController : ScSingleton<VibrationController>
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        public static readonly bool isAndroid = true;
#else
        public static AndroidJavaClass unityPlayer;
        public static AndroidJavaObject currentActivity;
        public static AndroidJavaObject vibrator;
        public static readonly bool isAndroid = false;
#endif
        [SerializeField]
        private bool isMute = false;
        public bool IsMute
        {
            get => isMute;
            set
            {
                if (value == false)
                    Cancel();

                isMute = value;
            }
        }

        public void VibrateM(long timeMillisecond)
        {
            if (IsMute)
                return;

            if (isAndroid)
            {
                vibrator.Call("vibrate", timeMillisecond);
            }
            else
            {
                Handheld.Vibrate();
            }
        }

        public void VibrateS(float timeSeconds)
        {
            if (IsMute)
                return;

            VibrateM((long)(timeSeconds * 1000f));
        }

        public void Cancel()
        {
            if (isAndroid)
                vibrator.Call("cancel");
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            IsMute = isMute;
        }
#endif
    }
}
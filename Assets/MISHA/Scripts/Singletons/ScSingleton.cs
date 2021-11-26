using UnityEngine;
using UnityEngine.SceneManagement;

namespace DCFAEngine.Singletons
{
    public class ScSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object m_Lock = new object();
        private static T m_Instance;


        public static T Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = (T)FindObjectOfType(typeof(T));

                        if (m_Instance == null)
                        {
                            var singletonObject = new GameObject();
                            m_Instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (ScSingleton)";

                            if (singletonObject.transform.parent == null)
                                DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return m_Instance;
                }
            }
        }
    }
}
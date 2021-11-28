
namespace DCFAEngine.Singletons
{
    public abstract class Singleton<T> where T : new()
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
                        m_Instance = new T();
                    }
                    return m_Instance;
                }
            }
        }
    }
}

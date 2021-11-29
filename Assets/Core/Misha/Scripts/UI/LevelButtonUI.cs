using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MISHA.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelButtonUI : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        public void OnClick()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

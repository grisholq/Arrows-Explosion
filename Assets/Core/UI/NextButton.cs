using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelsSceneIndexes.MAIN_MENU);
    }
}
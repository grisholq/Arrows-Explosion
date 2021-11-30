using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void LoadNextLevel()
    {
        Debug.Log(2);
        SceneManager.LoadScene(LevelsSceneIndexes.MAIN_MENU);
    }
}
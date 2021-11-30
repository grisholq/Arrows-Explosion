using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void RetryLevel()
    {
        Debug.Log(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
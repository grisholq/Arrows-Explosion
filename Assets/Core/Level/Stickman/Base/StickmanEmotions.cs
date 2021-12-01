using UnityEngine;
using DCFAEngine;

public class StickmanEmotions : MonoBehaviour
{
    [SerializeField] private SmileController _smileController;
    //[SerializeField][Range(0f, 1f)] private float _chance;
    //[SerializeField] private Sprite[] variations;
    
    public void ShowEmojy()
    {
        //if (Random.Range(0f, 1f) > _chance)
        //    return; 
        //
        //_smileRenderer.sprite = variations[Random.Range(0, variations.Length)];
        //_smileRenderer.gameObject.SetActive(true);

        _smileController.PlaySmile();
    }
}
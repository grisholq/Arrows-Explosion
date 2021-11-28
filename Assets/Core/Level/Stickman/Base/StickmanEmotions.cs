using UnityEngine;

public class StickmanEmotions : MonoBehaviour
{
    [SerializeField] private Transform _smile; 
    
    public void ShowEmojy()
    {
        _smile.gameObject.SetActive(true);
    }
}
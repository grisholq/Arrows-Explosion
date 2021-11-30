using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void Play()
    {
        
        _audioSource.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileController : MonoBehaviour
{
    private const int MAX_QUANYITY = 3;
    private readonly static List<SmileController> enabledSmiles = new List<SmileController>();
    [SerializeField]
    private SmileAnimator smileAnimator;
    [SerializeField] private Sprite[] variations;


    public void PlaySmile()
    {
        if (enabledSmiles.Count > MAX_QUANYITY)
            return;

        smileAnimator.Play(variations[Random.Range(0, variations.Length)]);
        enabledSmiles.Add(this);
    }
    public void StopSmile()
    {
        enabledSmiles.Remove(this);
    }
}

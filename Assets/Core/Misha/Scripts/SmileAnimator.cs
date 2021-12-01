using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SmileAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Ini();
    }

    private bool isIni = false;
    private void Ini()
    {
        if (isIni)
            return;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isIni = true;
    }

    public void Play(Sprite smile)
    {
        Ini();
        spriteRenderer.sprite = smile;
        animator.SetTrigger("Showed"+Random.Range(0, 4));
    }

    public void Stop()
    {
        animator.SetTrigger("Hidden");
    }
}


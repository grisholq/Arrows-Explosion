using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanDeathAnimation : MonoBehaviour
{
    [SerializeField]
    private StickmanStability stickmanStability;

    private StickmanAnimatorInterface animator;

    private void Awake()
    {
        animator = GetComponent<StickmanAnimatorInterface>();
        stickmanStability.Destabilized += StickmanDestabilized;
    }

    private void StickmanDestabilized()
    {
        animator.Death();
    }
}

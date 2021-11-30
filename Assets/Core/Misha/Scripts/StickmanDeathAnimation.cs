using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanDeathAnimation : MonoBehaviour
{
    [SerializeField]
    private StickmanStability stickmanStability;

    private StickmanAnimatorInterface animator;


    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;

    [SerializeField]
    private Material disableMaterial;


    [SerializeField]
    private ParticleSystem bloodParticles;

    private void Awake()
    {
        animator = GetComponent<StickmanAnimatorInterface>();
        stickmanStability.Destabilized += StickmanDestabilized;
        isRunned = false;
    }

    private bool isRunned;

    private void StickmanDestabilized()
    {
        if (isRunned)
            return;

        isRunned = true;
        meshRenderer.material = disableMaterial;
        bloodParticles.Play();
        //animator.Death();
    }
}

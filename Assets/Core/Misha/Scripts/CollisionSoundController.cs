using DCFAEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionSoundController : MonoBehaviour, IDamagable
{
    [Layer(true)][SerializeField]
    private int layersMask;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    [SerializeField]
    private GameSound gameSound;
    [SerializeField]
    private RangedFloat volumeRange = new RangedFloat(0f, 5f);
    [SerializeField]
    private float maxVolume = 1f;


    private bool isSleeped = true;
    private float volume;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSleeped)
            return; 
        int bitlayer;
        if (collision.rigidbody != null)
            bitlayer = 1 << collision.rigidbody.gameObject.layer;
        else
            bitlayer = 1 << collision.gameObject.layer;

        if ((layersMask & bitlayer) != bitlayer)
            return;

       
        float speed = Vector3.Distance(Vector3.zero, lateVelocity);
        PlaySound(volumeRange.MathPercent(Mathf.Min(volumeRange.max, speed)) * maxVolume);
    }

    private Vector3 lateVelocity;
    private void LateUpdate()
    {
        isSleeped = rigidbody.IsSleeping() || rigidbody.isKinematic;

        lateVelocity = rigidbody.velocity;
    }

    private void PlaySound(float volume)
    {
        audioSource.volume = volume;
        gameSound.Play(audioSource);
    }

    void IDamagable.RecieveDamage(float damage)
    {
        PlaySound(volumeRange.max);
    }
}

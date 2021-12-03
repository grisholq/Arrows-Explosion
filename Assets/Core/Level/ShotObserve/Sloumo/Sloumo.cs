using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using DCFAEngine;

public class Sloumo : Singleton<Sloumo>
{
    private const float _normalTimeScale = 1;
    [SerializeField]
    private float _slowTimeScale = 0.35f;
    [SerializeField]
    private AudioMixer mainAudioMixer;
    [SerializeField]
    private float pithFactor = 0.5f;


    private float defaultFixedDeltaTime;

    private void Awake()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void ActivateFor(float time)
    {
        SetSlowTime();
        Timer period = new Timer(time);
        period.Expired += SetNormalTime;
        period.Launch();
    }

    public void Activate()
    {
        SetSlowTime();
    }

    public void Deactivate()
    {
        SetNormalTime();
    }

    public void StopTime()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }



    private float time = 1f;
    private Coroutine coroutine;
    private float toSlowSpeed = 5f;
    private float toNormalSpeed = 1.6f;

    private void SetSlowTime()
    {
        float pith = _normalTimeScale - ((_normalTimeScale - _slowTimeScale) * pithFactor);
        mainAudioMixer.SetFloat("Pith", pith);
        //Time.timeScale = _slowTimeScale;
        //Time.fixedDeltaTime = defaultFixedDeltaTime * (_slowTimeScale / _normalTimeScale) * 1.5f;

        if (coroutine != null)
            StopCoroutine(coroutine);

        StartCoroutine(ToSlow());
    }
    private IEnumerator ToSlow()
    {
        while (time > 0f)
        {
            time -= Time.deltaTime * toSlowSpeed;
            SetVolume(time);
            yield return null;
        }
        time = 0f;
        SetVolume(time);
    }

    private void SetNormalTime()
    {
        mainAudioMixer.SetFloat("Pith", _normalTimeScale);
        //Time.timeScale = _normalTimeScale;
        //Time.fixedDeltaTime = defaultFixedDeltaTime;

        if (coroutine != null)
            StopCoroutine(coroutine);

        StartCoroutine(ToNormal());
    }
    private IEnumerator ToNormal()
    {
        while (time < 1f)
        {
            time += Time.deltaTime * toNormalSpeed;
            SetVolume(time);
            yield return null;
        }
        time = 1;
        SetVolume(time);
    }

    private void SetVolume(float percent)
    {
        RangedFloat rangedFloat = new RangedFloat();
        rangedFloat.min = _slowTimeScale;
        rangedFloat.SetMax(_normalTimeScale);

        Time.timeScale = rangedFloat.Evaluate(percent);
        Time.fixedDeltaTime = defaultFixedDeltaTime * (Time.timeScale / _normalTimeScale) * 1.5f;
    }

    private void OnDestroy()
    {
        Time.timeScale = _normalTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
    }   
}
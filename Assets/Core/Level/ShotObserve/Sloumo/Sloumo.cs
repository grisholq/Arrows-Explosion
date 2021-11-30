using UnityEngine;
using UnityEngine.Audio;

public class Sloumo : Singleton<Sloumo>
{
    private const float _normalTimeScale = 1;
    //private const float _normalPhysicsStep = 0.02f;
    [SerializeField]
    private float _slowTimeScale = 0.35f;
    //private const float _slowPhysicsStep = 0.01f;
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

    private void SetSlowTime()
    {
        float pith = _normalTimeScale - ((_normalTimeScale - _slowTimeScale) * pithFactor);
        mainAudioMixer.SetFloat("Pith", pith);
        Time.timeScale = _slowTimeScale;
        //Time.fixedDeltaTime = _slowPhysicsStep;
        Time.fixedDeltaTime = defaultFixedDeltaTime * (_slowTimeScale / _normalTimeScale) * 1.5f;
    }

    private void SetNormalTime()
    {
        mainAudioMixer.SetFloat("Pith", _normalTimeScale);
        Time.timeScale = _normalTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime;// _normalPhysicsStep;
    }
}
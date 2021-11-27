using UnityEngine;

public class Sloumo : Singleton<Sloumo>
{
    private const float _normalTimeScale = 1;
    private const float _normalPhysicsStep = 0.02f;

    private const float _slowTimeScale = 0.35f;
    private const float _slowPhysicsStep = 0.01f;

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

    private void SetSlowTime()
    {
        Time.timeScale = _slowTimeScale;
        Time.fixedDeltaTime = _slowPhysicsStep;
    }

    private void SetNormalTime()
    {
        Time.timeScale = _normalTimeScale;
        Time.fixedDeltaTime = _normalPhysicsStep;
    }
}
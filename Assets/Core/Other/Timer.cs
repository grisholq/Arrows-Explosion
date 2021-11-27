using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    public event UnityAction Expired;

    private Coroutine _timerWork;
    private float _period;

    public Timer(float period)
    {
        _period = period;
    }

    public void Launch()
    {
        _timerWork = 
            GlobalMono.Instance.StartCoroutine(TimerWork(_period));
    }

    public void Stop()
    {
        if (_timerWork == null) return;
        GlobalMono.Instance.StopCoroutine(_timerWork);
    }

    private IEnumerator TimerWork(float time)
    {
        yield return new WaitForSeconds(time);
        Expired?.Invoke();
    }
}
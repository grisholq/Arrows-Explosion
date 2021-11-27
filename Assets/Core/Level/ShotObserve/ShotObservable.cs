using UnityEngine;

public class ShotObservable : CameraObservable
{
    [SerializeField, Range(0, 255)] private int _priority;

    public int Priority { get; protected set; }

    protected override void Inizialize()
    {
        base.Inizialize();
        Priority = _priority;
    }

    public void AddAsShotObservable()
    {
        ShotObserver.Instance.AddObservedObject(this);
    }

    public void RemoveAsShotObservable()
    {
        ShotObserver.Instance.RemoveObservedObject(this);
    }
}
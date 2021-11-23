using UnityEngine;
using UnityEngine.Events;

public interface ISlideInput
{
    public event UnityAction Started;
    public event UnityAction Moved;
    public event UnityAction Ended;
    public event UnityAction<Vector3> DeltaChanged;

    public Vector3 Start { get; }
    public Vector3 End { get; }
    public Vector3 Delta { get; }
}
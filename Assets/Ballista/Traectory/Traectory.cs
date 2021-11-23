using System;
using UnityEngine;

public struct Traectory
{
    public Vector3 Start;
    public Vector3 End;
    public float DefaultHeight;
    public float Distance;
    public Vector3 Direction;
    public IFunction Function;

    public Vector3 GetPointAt(float progress)
    {
        if(progress < 0)
        {
            throw new Exception("Invalid progress value");
        }

        Vector3 point = new Vector3();
        point = Start + Direction * progress;
        point.y = DefaultHeight + Function.Calculate(progress);

        return point;
    }
}
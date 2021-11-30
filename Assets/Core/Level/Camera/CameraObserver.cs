using UnityEngine;
using System.Collections.Generic;

public class CameraObserver : Singleton<CameraObserver>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;

    public CameraObservable Current { get; set; }
  
    private void LateUpdate()
    {
        Observe();
    }

    public void Observe()
    {
        if (Current == null) return;
        ChangePosition();
        ChangeRotation();       
    }

    private void ChangeRotation()
    {
        float angularSpeed = Time.unscaledDeltaTime * _angularSpeed * Current.SpeedMultiplier;

        if (Current.LookAtObject)
        {
            transform.LookAt(Current.Transform);
            transform.eulerAngles += Current.LookAtEulers;
        }
        else
        {
            transform.eulerAngles = RotateTowards(Current.Eulers);         
        }
    }

    private Vector3 RotateTowards(Vector3 target)
    {
        Vector3 eulers = transform.eulerAngles;
        float speed = Time.unscaledDeltaTime * _angularSpeed * Current.SpeedMultiplier;

        eulers.x = Mathf.MoveTowardsAngle(eulers.x, target.x, speed);
        eulers.y = Mathf.MoveTowardsAngle(eulers.y, target.y, speed);
        eulers.z = Mathf.MoveTowardsAngle(eulers.z, target.z, speed);

        return eulers;
    }

    private void ChangePosition()
    {
        float speed = _speed * Current.SpeedMultiplier * Time.unscaledDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Current.CameraPosition, speed);
    }
}
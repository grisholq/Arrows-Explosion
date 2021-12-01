using UnityEngine;
using System.Collections.Generic;

public class CameraObserver : Singleton<CameraObserver>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;

    private CameraObservable _current;
    public CameraObservable Current
    {
        get => _current;
        set
        {
            _current = value;

            _lastPosition = transform.position;
            _lastEulers = transform.eulerAngles;

            coveredDistance = 0f;
            distance = Vector3.Distance(_lastPosition, Current.transform.position);
        }
    }
    public Vector3 _lastPosition;
    public Vector3 _lastEulers;
    private float distance;
    private float coveredDistance;

    private void LateUpdate()
    {
        Observe();
    }

    public void Observe()
    {
        if (Current == null) return;

        coveredDistance += _speed * Current.SpeedMultiplier * Time.unscaledDeltaTime;

        ChangePosition();
        ChangeRotation();       
    }

    private void ChangeRotation()
    {
        if (Current.LookAtObject)
        {
            transform.LookAt(Current.Transform);
            transform.eulerAngles += Current.LookAtEulers;
        }
        else
        {
            transform.eulerAngles = RotateTowards(_lastEulers, Current.Eulers, coveredDistance / distance);         
        }
    }

    private Vector3 RotateTowards(Vector3 origin, Vector3 target, float coveredPercent)
    {
        Vector3 eulers = transform.eulerAngles;
        eulers.x = Mathf.LerpAngle(origin.x, target.x, coveredPercent);
        eulers.y = Mathf.LerpAngle(origin.y, target.y, coveredPercent);
        eulers.z = Mathf.LerpAngle(origin.z, target.z, coveredPercent);

        return eulers;
    }

    private void ChangePosition()
    {
        transform.position = Vector3.Lerp(_lastPosition, Current.CameraPosition, coveredDistance / distance);
    }
}
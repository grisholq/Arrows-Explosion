using UnityEngine;
using System.Collections.Generic;

public class CameraObserver : Singleton<CameraObserver>
{
    [SerializeField] private float _speed;

    public CameraObservable Current { get; set; }
  
    private void LateUpdate()
    {
        Observe();
    }

    public void Observe()
    {
        if (Current == null) return;

        transform.position = Vector3.MoveTowards(transform.position, Current.CameraPosition, _speed * Time.unscaledDeltaTime);

        if (Current.LookAtObject)
        {
            transform.LookAt(Current.Transform);
            transform.eulerAngles += Current.LookAtEulers;
        }
        else 
        {
            transform.eulerAngles = Current.Eulers;
        }       
    }
}
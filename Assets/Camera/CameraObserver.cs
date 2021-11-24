using UnityEngine;
using System.Collections.Generic;

public class CameraObserver : Singleton<CameraObserver>
{
    [SerializeField] private float _speed;

    public LinkedList<CameraObservable> ObservableObjects { get; set; }

    public CameraObservable Current { get; private set; }

    private void LateUpdate()
    {
        Observe();
    }

    public void Observe()
    {
        if (Current == null) return;

        transform.position = Vector3.MoveTowards(transform.position, Current.CameraPosition, _speed * Time.deltaTime);

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

    public void AddObservableObject(CameraObservable observable)
    {
        if(observable.Priority > Current.Priority || Current == null)
        {
            Current = observable;
        }

        ObservableObjects.AddLast(observable);
    }

    public void RemoveObservableObject(CameraObservable observable)
    {
        ObservableObjects.Remove(observable);

        if(Current == observable)
        {
            Current = GetObservableObjectWithHighestPriority();
        }
    }

    private CameraObservable GetObservableObjectWithHighestPriority()
    {
        if (ObservableObjects.Count == 0) return null;

        CameraObservable buffer = null;
        int maxPriority = 0;

        foreach (var observable in ObservableObjects)
        {
            if(observable.Priority > maxPriority)
            {
                maxPriority = observable.Priority;
                buffer = observable;
            }
        }

        return buffer;
    }
}
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ShotObserverObjects
{
    private LinkedList<ShotObservable> _observableObjects;

    public event UnityAction<ShotObservable> CurrentChanged;
    public event UnityAction NoObjectsLeft;

    public ShotObservable Current { get; private set; }

    public ShotObserverObjects()
    {
        _observableObjects = new LinkedList<ShotObservable>();
    }

    public void AddShotObservable(ShotObservable observable)
    {
        if(Current == null || Current.Priority < observable.Priority)
        {
            Current = observable;
            CurrentChanged?.Invoke(observable);
        }

        _observableObjects.AddLast(observable);
    }

    public void RemoveShotObservable(ShotObservable observable)
    {
        _observableObjects.Remove(observable);

        if(_observableObjects.Count == 0)
        {
            NoObjectsLeft?.Invoke();
            return;
        }

        if(Current == observable)
        {
            Current = GetHighestPriorityShotObservable();
            CurrentChanged?.Invoke(observable);
        }
    }

    public void ClearShotObservables()
    {
        _observableObjects.Clear();
        Current = null;
    }
    
    private ShotObservable GetHighestPriorityShotObservable()
    {
        ShotObservable highest = null;
        int maxPriority = 0;

        foreach (var observable in _observableObjects)
        {
            if(observable.Priority > maxPriority)
            {
                highest = observable;
                maxPriority = observable.Priority;
            }
        }

        return highest;
    }
}
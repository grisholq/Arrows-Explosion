using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    [SerializeField] private Transform _prefab;

    private Queue<Transform> _poolObjects = new Queue<Transform>();

    public virtual Transform Retrieve()
    {
        if(_poolObjects.Count == 0)
        {
            return Instantiate(_prefab.transform);
        }

        Transform poolObject = _poolObjects.Dequeue();
        poolObject.gameObject.SetActive(true);
        return poolObject;
    }

    public virtual void Return(Transform poolObject)
    {
        _poolObjects.Enqueue(poolObject);
        poolObject.gameObject.SetActive(false);
    }
}
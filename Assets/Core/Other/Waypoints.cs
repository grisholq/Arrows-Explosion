using UnityEngine;
using System.Collections.Generic;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] _waypointsTransforms;

    public IEnumerable<Vector3> Points => _waypoints;
    public int Amount => _waypoints.Length;

    private Vector3[] _waypoints;

    private void Awake()
    {        
        Inizialize();
    }

    private void Inizialize()
    {
        _waypoints = new Vector3[transform.childCount];

        for (int i = 0; i < _waypointsTransforms.Length; i++)
        {
            _waypoints[i] = transform.GetChild(i).position;
        }
    }
}
using UnityEngine;

public class WinCameraObservable : CameraObservable
{
    private void Awake()
    {
        CameraPosition = transform.position;
        Eulers = transform.eulerAngles;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);      
    }
}
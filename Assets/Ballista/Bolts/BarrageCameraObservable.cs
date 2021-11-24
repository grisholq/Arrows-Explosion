using UnityEngine;

public class BarrageCameraObservable : CameraObservable
{ 
    private void Update()
    {
        CameraPosition = transform.position + new Vector3(2, 2, -6);
    }
}
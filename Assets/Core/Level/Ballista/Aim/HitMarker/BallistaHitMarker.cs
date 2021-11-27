using UnityEngine;

public class BallistaHitMarker : MonoBehaviour
{
    [SerializeField] private Transform _marker;
    [SerializeField] private float _step;

    //public void PositionMarker(Traectory traectory)
    //{
    //    float step = traectory.Distance / _steps;

    //    for (int i = 1; i < _steps; i++)
    //    {
    //        Vector3 Start = traectory.GetPointAt(step * (i - 1));
    //        Vector3 End = traectory.GetPointAt(step * i);

    //        if (Physics.Raycast(Start, End - Start, out RaycastHit hit, Vector3.Magnitude(End - Start)))
    //        {
    //            _marker.position = hit.point;
    //            return;
    //        }
    //    }
    //}

    public void PositionMarker(Traectory traectory)
    {
        for (float i = _step; i < traectory.Distance; i += _step)
        {
            Vector3 Start = traectory.GetPointAt(i - _step);
            Vector3 End = traectory.GetPointAt(i);

            if (Physics.Raycast(Start, End - Start, out RaycastHit hit, Vector3.Magnitude(End - Start)))
            {
                _marker.position = hit.point;
                return;
            }
        }
    }

    public void EnableAimMarker()
    {
        _marker.gameObject.SetActive(true);
    }

    public void DisableAimMarker()
    {
        _marker.gameObject.SetActive(false);
    }
}
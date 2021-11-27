using UnityEngine;

public class AimHitMarker : MonoBehaviour
{
    [SerializeField] private int _steps;
    [SerializeField] private GameObject _inputGO;
    [SerializeField] private BallistaTraectoryCalculator _traectory;
    [SerializeField] private Transform _marker;

    private ISlideInput _input;

    private void Awake()
    {
        _input = _inputGO.GetComponent<ISlideInput>();
        _input.Started += EnableMarker;
        _traectory.TraectoryChanged += PositionMarker;
        _input.Ended += DisableMarker;
    }

    private void PositionMarker(Traectory traectory)
    {
        float step = traectory.Distance / _steps;

        for (int i = 1; i < _steps; i++)
        {
            Vector3 Start = traectory.GetPointAt(step * (i - 1));
            Vector3 End = traectory.GetPointAt(step * i);

            if (Physics.Raycast(Start, End - Start, out RaycastHit hit, Vector3.Magnitude(End - Start)))
            {
                _marker.position = hit.point;
                return;
            }
        }
    }

    private void EnableMarker()
    {
        _marker.gameObject.SetActive(true);
    }

    private void DisableMarker()
    {
        _marker.gameObject.SetActive(false);
    }
}
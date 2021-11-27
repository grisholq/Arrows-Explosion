using UnityEngine;

public class BallistaCameraObservable : CameraObservable
{
    [SerializeField] private float _coeffX;
    [SerializeField] private float _coeffZ;
    [SerializeField] private float _distance;
    [SerializeField] private float _height;

    private void Update()
    {
        CalculatePosition();
        CalculateRotation();
    }

    private void CalculatePosition()
    {
        Vector3 position = -transform.forward;
        position.x *= _coeffX;
        position.z *= _coeffZ;
        position = position.normalized * _distance;
        position += Vector3.up * _height;
        CameraPosition = transform.position + position;
    }

    private void CalculateRotation()
    {
        Vector3 direction = transform.position - CameraPosition;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        Vector3 eulers = Eulers;
        eulers.y = -(angle - 90);
        Eulers = eulers;
    }
}
using UnityEngine;

public class BallistaRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _slideInputMono;

    private ISlideInput _slideInput;

    private float _targetAngle = 180;

    private void Awake()
    {
        _slideInput = _slideInputMono.GetComponent<ISlideInput>();
        _slideInput.DeltaChanged += UpdateTargetAngle;
        _slideInput.Ended += ResetTargetAngle;
    }

    private void Update()
    {
        Vector3 eulers = transform.eulerAngles;
        eulers.y = Mathf.MoveTowardsAngle(eulers.y, _targetAngle, _rotationSpeed * Time.deltaTime);
        transform.eulerAngles = eulers;
    }

    private void UpdateTargetAngle(Vector3 input)
    {
        _targetAngle = GetAngleFromDirection(-input);
    }
    
    private void ResetTargetAngle()
    {
        _targetAngle = 180;
    }

    private float GetAngleFromDirection(Vector3 direction)
    {
        return -Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + 270;
    }
}
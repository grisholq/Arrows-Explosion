using UnityEngine;

public class CameraObservable : MonoBehaviour
{
    [SerializeField] protected bool _isDefault;
    [SerializeField] protected Vector3 _position;
    [SerializeField] protected Vector3 _eulers;
    [SerializeField] protected bool _lookAtObject;
    [SerializeField] protected Vector3 _lookAtEulers;
    [SerializeField, Range(0.1f, 2)] protected float _speedMultiplier;

    public Transform Transform => transform;
    public Vector3 CameraPosition { get; protected set; }
    public Vector3 Eulers { get; protected set; }
    public bool LookAtObject { get; protected set; }
    public Vector3 LookAtEulers { get; protected set; }
    public float SpeedMultiplier => _speedMultiplier;

    private void Awake()
    {
        Inizialize();
    }

    protected virtual void Inizialize()
    {
        if(_isDefault) SetAsCameraObservable();
        CameraPosition = _position;
        Eulers = _eulers;
        LookAtObject = _lookAtObject;
        LookAtEulers = _lookAtEulers;
    }

    public void SetAsCameraObservable()
    {
        CameraObserver.Instance.Current = this;
    }
}
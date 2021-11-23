using UnityEngine;

public class CameraObservable : MonoBehaviour
{
    [SerializeField] private bool _isDefault;
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _eulers;
    [SerializeField] private bool _lookAtObject;
    [SerializeField] private Vector3 _lookAtEulers;
    [SerializeField, Range(0, 255)] private int _priority;

    public Transform Transform => transform;
    public Vector3 CameraPosition { get; protected set; }
    public Vector3 Eulers { get; protected set; }
    public bool LookAtObject { get; protected set; }
    public Vector3 LookAtEulers { get; protected set; }

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
        CameraObserver.Instance.CameraObservable = this;
    }
}
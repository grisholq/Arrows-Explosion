using UnityEngine;

public class CameraObservable : MonoBehaviour
{
    [SerializeField] protected bool _isDefault;
    [SerializeField] protected Vector3 _position;
    [SerializeField] protected Vector3 _eulers;
    [SerializeField] protected bool _lookAtObject;
    [SerializeField] protected Vector3 _lookAtEulers;
    [SerializeField, Range(0, 255)] protected int _priority;

    public Transform Transform => transform;
    public Vector3 CameraPosition { get; protected set; }
    public Vector3 Eulers { get; protected set; }
    public bool LookAtObject { get; protected set; }
    public Vector3 LookAtEulers { get; protected set; }
    public int Priority => _priority;

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
        CameraObserver.Instance.AddObservableObject(this);
    }

    public void ResetAsCameraObservable()
    {
        CameraObserver.Instance.RemoveObservableObject(this);
    }
}
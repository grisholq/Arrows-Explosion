using UnityEngine;
using UnityEngine.Events;

public class MouseSlideInput : MonoBehaviour, ISlideInput
{
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    public event UnityAction Started;
    public event UnityAction Moved;
    public event UnityAction Ended;
    public event UnityAction<Vector3> DeltaChanged;

    public Vector3 Start { get; private set; }
    public Vector3 End { get; private set; }
    public Vector3 Delta
    {
        get
        {
            Vector3 delta = End - Start;
            return new Vector3(delta.x, 0, delta.y);
        }
    }

    private Vector3 _slideLastPosition;
    private bool _slideStarted = false;
    private bool _enabled = true;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (_enabled == false) return;

        if(SlideStarted())
        {          
            StartSlide();
            return;
        }

        if(SlideEnded())
        {         
            EndSlide();          
            return;
        }

        if(SlidePositionMoved())
        {
            MoveSlide();
        }

        UpdateSlideLastPosition();
    }

    private bool SlidePositionMoved()
    {
        return Input.mousePosition != _slideLastPosition && _slideStarted;
    }

    private bool SlideStarted()
    {
        return Input.GetMouseButtonDown(0) && _slideStarted == false;
    }

    private bool SlideEnded()
    {
        return Input.GetMouseButtonUp(0) && _slideStarted;
    }

    private void StartSlide()
    {
        Start = GetSlideCurrentPosition();
        End = Start - new Vector3(0, 1f);
        Started?.Invoke();
        _slideStarted = true;
    }

    private void MoveSlide()
    {
        Vector3 newEnd = GetSlideCurrentPosition();

        if(IsSlideEndCorrent(newEnd))
        {
            End = newEnd;
            Moved?.Invoke();
            DeltaChanged?.Invoke(Delta);
        }
        else
        {
            CorrectStartAndEnd(newEnd);
        }
    }

    private void EndSlide()
    {
        Ended?.Invoke();                
        _slideStarted = false;
    }

    private bool IsSlideEndCorrent(Vector3 end)
    {
        Vector3 delta = end - Start;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        return angle > _minAngle && angle < _maxAngle;
    }

    private void CorrectStartAndEnd(Vector3 newEnd)
    {
        Vector3 delta = Start - End;
        Start = newEnd + delta;
        End = newEnd;
    }

    private Vector3 GetSlideCurrentPosition()
    {
        return Input.mousePosition;
    }

    private void UpdateSlideLastPosition()
    {
        _slideLastPosition = Input.mousePosition;
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
    }
}
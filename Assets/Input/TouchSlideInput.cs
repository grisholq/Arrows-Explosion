using UnityEngine;
using UnityEngine.Events;

public class TouchSlideInput : MonoBehaviour, ISlideInput
{
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

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

    public event UnityAction Started;
    public event UnityAction Moved;
    public event UnityAction Ended;
    public event UnityAction<Vector3> DeltaChanged;

    private Vector3 _slideLastPosition;
    private bool _slideStarted = false;

    private void Update()
    {
        if (Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            StartSlide(touch);
        }

        if (touch.phase == TouchPhase.Ended)
        {
            EndSlide();
        }

        if (touch.phase == TouchPhase.Moved)
        {
            MoveSlide(touch);
        }

        UpdateSlideLastPosition();
    }

    private void StartSlide(Touch touch)
    {
        Start = (Vector3)touch.position;
        End = Start;
        Started?.Invoke();
        _slideStarted = true;
    }

    private void MoveSlide(Touch touch)
    {
        Vector3 newEnd = (Vector3)touch.position;

        if (IsSlideEndCorrent(newEnd))
        {
            End = newEnd;
            Moved?.Invoke();
            DeltaChanged?.Invoke(Delta);
        }
    }

    private void EndSlide()
    {
        if (IsSlideEndCorrent(End))
        {
            Ended?.Invoke();
        }
        _slideStarted = false;
    }

    private bool IsSlideEndCorrent(Vector3 end)
    {
        Vector3 delta = end - Start;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        return angle > _minAngle && angle < _maxAngle;
    }

    private Vector3 GetSlideCurrentPosition()
    {
        return Input.mousePosition;
    }

    private void UpdateSlideLastPosition()
    {
        _slideLastPosition = Input.mousePosition;
    }
}
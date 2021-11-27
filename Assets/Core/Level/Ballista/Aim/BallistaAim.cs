using UnityEngine;

[RequireComponent(typeof(BallistaTraectoryCalculator))]
public class BallistaAim : MonoBehaviour
{
    [SerializeField] private float _aimDistancePercent;
    [SerializeField] private AimBalls _aimBalls;
    [SerializeField] private GameObject _inputMono;

    private BallistaTraectoryCalculator _traectoryCalculator;
    private ISlideInput _slideInput;
    private bool _drawing = false;

    private void Awake()
    {
        _traectoryCalculator = GetComponent<BallistaTraectoryCalculator>();
        _traectoryCalculator.TraectoryChanged += DrawTraectory;

        _slideInput = _inputMono.GetComponent<ISlideInput>();
        _slideInput.Started += StartDrawingTraectory;
        _slideInput.Ended += EndDrawingTraectory;
    }

    private void Start()
    {
        _aimBalls.DeactivateBalls();
    }

    public void StartDrawingTraectory()
    {
        _drawing = true;
        _aimBalls.ActivateBalls();
    }

    public void EndDrawingTraectory()
    {
        _drawing = false;
        _aimBalls.DeactivateBalls();
    }

    public void DrawTraectory(Traectory traectory)
    {
        float step = (traectory.Distance * (1 + _aimDistancePercent)) / _aimBalls.Amount;

        for (float i = 0; i < _aimBalls.Amount; i++)
        {
            PositionBall(traectory.GetPointAt(i * step));
        }
    }

    private void PositionBall(Vector3 position)
    {
        Transform ball = _aimBalls.GetBall();
        ball.position = position;
    }
}
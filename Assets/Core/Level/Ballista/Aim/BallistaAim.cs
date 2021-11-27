using UnityEngine;

public class BallistaAim : MonoBehaviour
{
    [SerializeField] private BallistaTraectoryCalculator _traectory;
    [SerializeField] private BallistaTraectoryDrawer _traectoryDrawer;
    [SerializeField] private BallistaHitMarker _hitMarker;
    [SerializeField] private GameObject _slideInput;

    private ISlideInput _input;

    private void Awake()
    {
        _input = _slideInput.GetComponent<ISlideInput>();

        _input.Started += EnableAim;
        _input.Started += _hitMarker.EnableAimMarker;

        _input.Ended += DisableAim;
        _input.Ended += _hitMarker.DisableAimMarker;
        _input.Ended += _traectoryDrawer.ReturnAimBallsToPool;
    }

    public void EnableAim()
    {
        _traectory.TraectoryChanged += _traectoryDrawer.DrawTraectory;
        _traectory.TraectoryChanged += _hitMarker.PositionMarker;
    }

    public void DisableAim()
    {
        _traectory.TraectoryChanged -= _traectoryDrawer.DrawTraectory;
        _traectory.TraectoryChanged -= _hitMarker.PositionMarker;
    }
}
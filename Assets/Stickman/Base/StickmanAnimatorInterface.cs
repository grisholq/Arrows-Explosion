using UnityEngine;

public class StickmanAnimatorInterface : AnimatorInterface
{
    [SerializeField] private StickmanMover _mover;
    [SerializeField] private StickmanStability _stability;
    [SerializeField] private string _movingParameter;

    private void Awake()
    {
        _stability.Destabilized += DisableAnimator;
        _mover.MovementResumed += Run;
        _mover.MovementStopped += StandIdle;
    }

    public void StandIdle()
    {
        if (_animator.enabled == false) return;
        _animator.SetBool(_movingParameter, false);
    }
    
    public void Run()
    {
        if (_animator.enabled == false) return;
        _animator.SetBool(_movingParameter, true);
    }
}
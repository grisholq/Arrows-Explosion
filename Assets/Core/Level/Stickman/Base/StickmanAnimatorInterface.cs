using UnityEngine;

public class StickmanAnimatorInterface : AnimatorInterface
{
    [SerializeField] private StickmanMover _mover;
    [SerializeField] private StickmanStability _stability;
    [SerializeField] private string _movingParameter;
    [SerializeField] private string _danceParameter;

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

    public void Death()
    {
        if (_animator.enabled == false) return;
        _animator.SetTrigger("Death");
    }
    
    public void Dance()
    {
        if (_animator.enabled == false) return;
        _animator.SetFloat("Salt", Random.Range(0f, 1f));
        _animator.SetTrigger("Dance");
    }
}
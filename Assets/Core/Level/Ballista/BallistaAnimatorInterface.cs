using UnityEngine;

public class BallistaAnimatorInterface : AnimatorInterface
{
    [SerializeField] private BallistaBoltShooter _shooter;
    [SerializeField] private string _shootParameter;

    private void Awake()
    {
        _shooter.Shooted += Shoot;
    }

    public void Shoot()
    {
        _animator.SetTrigger(_shootParameter);
    }
}
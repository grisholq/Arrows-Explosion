using UnityEngine;

public class BallistaAnimatorInterface : AnimatorInterface
{
    [SerializeField] private Ballista _ballista;
    [SerializeField] private string _shootParameter;

    private void Awake()
    {
        _ballista.Shot += Shoot;
    }

    public void Shoot()
    {
        _animator.SetTrigger(_shootParameter);
    }
}
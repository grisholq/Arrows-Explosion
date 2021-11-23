using UnityEngine;

public class AnimatorInterface : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    public void EnableAnimator()
    {
        _animator.enabled = true;
    }

    public void DisableAnimator()
    {
        _animator.enabled = false;
    }
}
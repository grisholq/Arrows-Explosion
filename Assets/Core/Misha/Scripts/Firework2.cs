using DCFAEngine;
using DCFAEngine.StateSystem;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(StateMachine))]
public class Firework2 : MonoBehaviour, IDamager, IDamagable
{
    private Animator animator;
    private StateMachine stateMachine;
    private Explosion explosion;
    private new Rigidbody rigidbody;

    public event UnityAction Launched;
    public event UnityAction Exploded;

    [Layer]
    [SerializeField]
    private int landscapeLayer;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxDistance;
    private float distance;
    [SerializeField]
    private SpecialEffect explosionPrefab;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        explosion = GetComponent<Explosion>();
        rigidbody = GetComponent<Rigidbody>();

        stateMachine.AddStateListeners(DefaultStates.IDLE, PreIdle, null, null);
        stateMachine.AddStateListeners(DefaultStates.ACTOVATED, PreActivated, UpdateActivated, null);
        stateMachine.AddStateListeners(DefaultStates.DEATH, PreDeath, null, null);

        stateMachine.SetNextState(DefaultStates.IDLE);
    }

    private void PreDeath()
    {
        Exploded?.Invoke();
        explosion.Explode();
        SpecialEffect explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionEffect.Show();
        Destroy(gameObject);
    }

    private void PreIdle()
    {
        animator.SetTrigger(DefaultAnimations.IDLE);
    }

    private void PreActivated()
    {
        Launched?.Invoke();
        animator.SetTrigger("Activated");
        rigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateMachine.GetCurrentState() == DefaultStates.ACTOVATED)
        {
            if(other.TryGetComponent(out IDamagable iDamagable))
            {
                iDamagable.RecieveDamage(1000f);
            }
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(transform.forward * moveSpeed * 200f);
            }
        }
    }

    private void UpdateActivated()
    {
        Vector3 pos = transform.position;
        Ray ray = new Ray(pos, transform.forward);
        distance += 1f * moveSpeed * Time.deltaTime;

        Debug.DrawRay(ray.origin + Vector3.up * 0.1f, ray.direction, Color.cyan, distance);
        //Debug.Break();

        if (maxDistance <= distance || Physics.Raycast(ray, moveSpeed * Time.deltaTime, 1 << landscapeLayer))
        {
            stateMachine.SetNextState(DefaultStates.DEATH);
        }

        Vector3 movepos = transform.position + ray.direction * moveSpeed * Time.deltaTime;
        transform.position =(movepos);
    }

    void IDamager.Damage(IDamagable damagable)
    {

    }

    void IDamagable.RecieveDamage(float damage)
    {
        if (stateMachine.GetCurrentState() == DefaultStates.IDLE)
            stateMachine.SetNextState(DefaultStates.ACTOVATED);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * (maxDistance - distance));
        Gizmos.DrawSphere(transform.position + transform.forward * (maxDistance - distance), 0.2f);
    }
}

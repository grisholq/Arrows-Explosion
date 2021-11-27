using UnityEngine;
using UnityEngine.Events;

public class BallistaTraectoryCalculator : MonoBehaviour
{
    [SerializeField] private GameObject _inputMono;
    [SerializeField] private float _peakHeight;
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _forceMultiplier;
    [SerializeField] private float _additionalDistance;

    public event UnityAction<Traectory> TraectoryChanged;

    public Traectory Current
    {
        get => _current;

        set
        {
            _current = value;
            TraectoryChanged?.Invoke(value);
        }
    }

    private Traectory _current;

    private ISlideInput _input;

    private void Awake()
    {
        _input = _inputMono.GetComponent<ISlideInput>();
        _input.DeltaChanged += CalculateTraectory;
    }

    public void CalculateTraectory(Vector3 force)
    {
        CalculateTraectory(force.normalized, force.magnitude);
    }

    public void CalculateTraectory(Vector3 direction, float force)
    {
        force = Mathf.Min(_minForce + force * _forceMultiplier, _maxForce);

        Traectory traectory = new Traectory();

        traectory.Start = transform.position;
        traectory.End = transform.position + direction * force;
        traectory.Distance = Vector3.Distance(traectory.Start, traectory.End) + _additionalDistance;
        traectory.DefaultHeight = transform.position.y;
        traectory.Direction = -direction;
        traectory.Function = GetTraectoryFunction(direction, force);

        Current = traectory;
    }

    private IFunction GetTraectoryFunction(Vector3 direction, float force)
    {
        Vector3 A = transform.position;
        Vector3 B = A + direction * force / 2;
        Vector3 C = B + Vector3.up * _peakHeight;

        float Cz = C.y;
        float Az = A.y;
        float Bforce = force / 2;
        float Af = 0;

        float a = (Cz - Az) / Mathf.Pow(Af - Bforce, 2);

        return new QuadraticEquation(-a, Bforce, _peakHeight);
    }
}
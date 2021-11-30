using UnityEngine;
using UnityEngine.Events;
using DCFAEngine;

public class BoltsBarrage : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event UnityAction Hit;

    private Traectory _traectory;
    private float _progress;

    private Bolt[] _bolts;
    [SerializeField]
    private float minHeight = -10f;

    private void Awake()
    {
        _bolts = GetComponentsInChildren<Bolt>();
        RandomizeBolts();
    }

    private void RandomizeBolts()
    {
        foreach (var bolt in _bolts)
        {
            bolt.transform.position += Random.insideUnitSphere * 0.1f;
        }
    }

    public void Launch(Traectory traectory)
    {
        _traectory = traectory;
        _progress = 0;
    }

    private void Update()
    {
        IncrementProgress();
        UpdatePosition();

        if(HasBolts() == false || transform.position.y <= minHeight)
        {
            Hit?.Invoke();
            Destroy(gameObject);
        }
    }

    private void IncrementProgress()
    {
        _progress += Time.deltaTime * _speed;
    }

    private void UpdatePosition()
    {
        Vector3 position = _traectory.GetPointAt(_progress);
        transform.LookAt(position);
        transform.position = position;
    }

    private bool HasBolts()
    {
        return transform.childCount > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position.XoZ() + Vector3.up*minHeight, new Vector3(10f, 1f, 10f));
    }
}
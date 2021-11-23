using UnityEngine;

public class BoltsBarrage : MonoBehaviour
{
    [SerializeField] private float speed;

    private Traectory _traectory;
    private float _progress;

    private Bolt[] _bolts;

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

        if(HasBolts() == false)
        {
            Destroy(gameObject);
        }
    }

    private void IncrementProgress()
    {
        _progress += Time.deltaTime * speed;
    }

    private void UpdatePosition()
    {
        Vector3 position = _traectory.GetPointAt(_progress);
        transform.LookAt(position);
        transform.position = position;
    }

    private bool HasBolts()
    {
        return transform.childCount != 0;
    }
}
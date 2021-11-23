using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    [SerializeField] private Transform _prefab;

    public Transform GetPrefab()
    {
        return Instantiate(_prefab);
    }
}
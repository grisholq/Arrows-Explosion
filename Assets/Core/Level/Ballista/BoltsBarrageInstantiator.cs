using UnityEngine;

public class BoltsBarrageInstantiator : PrefabInstantiator
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _rotation;

    public BoltsBarrage GetBoltsBarrage()
    {
        BoltsBarrage barrage = GetPrefab().GetComponent<BoltsBarrage>();
        barrage.transform.position = _position;
        barrage.transform.eulerAngles = _rotation;
        return barrage;
    }
}
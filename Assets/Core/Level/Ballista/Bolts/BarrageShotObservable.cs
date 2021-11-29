using UnityEngine;

[RequireComponent(typeof(BoltsBarrage))]
public class BarrageShotObservable : ShotObservable
{
    private BoltsBarrage _barrage;

    protected override void Inizialize()
    {
        base.Inizialize();
        _barrage = GetComponent<BoltsBarrage>();
        _barrage.Hit += RemoveAsShotObservable;
        AddAsShotObservable();       
    }

    private void Update()
    {
        CameraPosition = transform.position + new Vector3(4, 3, -8);
    }
}
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BallistaTraectoryCalculator))]
public class Ballista : MonoBehaviour
{
    [SerializeField] private float _reloadTime;
    [SerializeField] private BoltsBarrageInstantiator _barrageInstantiator;
    [SerializeField] private BallistaTraectoryCalculator _traectoryCalculator;
    [SerializeField] private GameObject _shotInput;

    //public event UnityAction Shot;
    //public event UnityAction Reloaded;
}
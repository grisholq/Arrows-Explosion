using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private UnityEvent _victory;
    [SerializeField] private UnityEvent _lose;
}
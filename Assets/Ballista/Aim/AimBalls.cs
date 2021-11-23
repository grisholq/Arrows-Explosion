using UnityEngine;
using System.Collections.Generic;

public class AimBalls : MonoBehaviour
{
    [SerializeField] private PrefabInstantiator _ballsInstantiator;
    [SerializeField] private int _amount;

    public int Amount => _amount;

    private Queue<Transform> _balls;

    private void Awake()
    {
        Inizialize();
    }

    private void Inizialize()
    {
        _balls = new Queue<Transform>();

        for (int i = 0; i < _amount; i++)
        {
            Transform ball = _ballsInstantiator.GetPrefab();
            ball.parent = transform;
            _balls.Enqueue(ball);
        }
    }

    public Transform GetBall()
    {
        Transform ball = _balls.Dequeue();
        _balls.Enqueue(ball);
        return ball;
    }

    public void ActivateBalls()
    {
        foreach (var ball in _balls)
        {
            ball.gameObject.SetActive(true);
        }
    }
    
    public void DeactivateBalls()
    {
        foreach (var ball in _balls)
        {
            ball.gameObject.SetActive(false);
        }
    }
}
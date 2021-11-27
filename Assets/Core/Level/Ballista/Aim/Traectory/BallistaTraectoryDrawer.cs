using UnityEngine;
using System.Collections.Generic;

public class BallistaTraectoryDrawer : MonoBehaviour
{
    [SerializeField] private AimBallsPool _ballsPool;
    [SerializeField] private float _distancePerBall;

    private List<Transform> _aimBalls = new List<Transform>();

    public void DrawTraectory(Traectory traectory)
    {
        ReturnAimBallsToPool();

        for (float i = 0; i < traectory.Distance; i += _distancePerBall)
        {
            Vector3 position = traectory.GetPointAt(i);
            Transform ball = _ballsPool.Retrieve();
            ball.position = position;
            _aimBalls.Add(ball);
        }
    }
    
    public void ReturnAimBallsToPool()
    {
        foreach (var ball in _aimBalls)
        {
            _ballsPool.Return(ball);
        }

        _aimBalls.Clear();
    }
}
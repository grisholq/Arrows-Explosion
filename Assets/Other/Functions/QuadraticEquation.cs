using UnityEngine;

public class QuadraticEquation : IFunction
{
    public float A { get; private set; }
    public float B { get; private set; }
    public float C { get; private set; }

    public QuadraticEquation(float a, float b, float c)
    {
        A = a;
        B = b;
        C = c;
    }

    public float Calculate(float x)
    {
        return A * Mathf.Pow(x - B, 2) + C;
    }
}
using UnityEngine;

public static class Vector3Extentions
{
    public static Vector3 ComponetsMultiply(this Vector3 vector, Vector3 multiplier)
    {
        Vector3 result = vector;
        result.x *= multiplier.x;
        result.y *= multiplier.y;
        result.z *= multiplier.z;
        return result;
    }
}
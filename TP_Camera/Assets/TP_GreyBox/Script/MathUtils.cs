using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 dir = b - a; 
        float length = dir.magnitude; 
        dir.Normalize(); 

        float dotProduct = Vector3.Dot(target - a, dir);

        dotProduct = Mathf.Clamp(dotProduct, 0, length);

        Vector3 nearestPoint = a + dir * dotProduct;

        return nearestPoint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SphereUtils
{
    public static Vector3[] EquidistantPoints(int count, float scale)
    {
        Vector3[] result = new Vector3[count];

        // Don't fully understand this, should rewrite it 
        // code acquired from https://forum.unity.com/threads/evenly-distributed-points-on-a-surface-of-a-sphere.26138/
        // ===================================================================================
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / count;
        float x, y, z, r, phi;

        for (var k = 0; k < count; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            result[k] = new Vector3(x, y, z) * (scale / 2);
        }

        // ===================================================================================
        
        return result;
    }
}

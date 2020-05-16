using System;
using UnityEngine;

public static class Vector3Extension
{
    public static Vector3Int AsVector3Int(this Vector3 source)
    {
        return new Vector3Int(Convert.ToInt32(Math.Round(source.x, 0)),
            Convert.ToInt32(Math.Round(source.y, 0)),
            Convert.ToInt32(Math.Round(source.z, 0)));
    }
}
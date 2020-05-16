using System;
using UnityEngine;

namespace PPop.Infrastructure.Helpers.Math 
{
    public static class Vector3Extension
    {
        public static Vector3Int AsVector3Int(this Vector3 source)
        {
            return new Vector3Int(Convert.ToInt32(System.Math.Round(source.x, 0)),
                Convert.ToInt32(System.Math.Round(source.y, 0)),
                Convert.ToInt32(System.Math.Round(source.z, 0)));
        }
    }
}
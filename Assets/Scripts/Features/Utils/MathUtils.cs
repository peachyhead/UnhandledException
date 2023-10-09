// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Utils
{
    public static class MathUtils
    {
        public static List<Vector3Int> GetAvailable(this Vector3Int bounds, 
            List<Vector3Int> occupied)
        {
            var points = Enumerable
                .Range(-bounds.x, bounds.x)
                .SelectMany(x =>
                    Enumerable.Range(-bounds.y, bounds.y)
                        .SelectMany(y =>
                            Enumerable.Range(-bounds.z, bounds.z)
                                .Select(z => new Vector3Int(x, y, z))
                        )
                )
                .ToList();
            
            return points.Except(occupied).ToList();
        }
    }
}
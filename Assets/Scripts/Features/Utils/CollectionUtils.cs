// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Utils
{
    public static class CollectionUtils 
    {
        public static T GetRandom<T>(this IReadOnlyList<T> list)
        {
            if (list != null && list.Any())
                return list[GetRandomIndex(list)];
            return default;
        }

        private static int GetRandomIndex<T>(this IReadOnlyCollection<T> list)
        {
            return Random.Range(0, list.Count);
        }
    }
}
// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;

namespace Features.Utils
{
    public static class SystemUtils
    {
        public static string GetGuidID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
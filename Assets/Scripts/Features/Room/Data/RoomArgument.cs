// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved 
// [2020]-[2023].

using UnityEngine;

namespace Features.Room.Data
{
    public struct RoomArgument
    {
        public Vector3 Position { get; }
        
        public RoomArgument(Vector3 position)
        {
            Position = position;
        }
    }
}
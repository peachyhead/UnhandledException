// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using UnityEngine;

namespace Features.Room.Data
{
    [Serializable]
    public class RoomGenerationData
    {
        [SerializeField] private RoomID _id;
        [SerializeField] private int _roomCount;

        public RoomID ID => _id;
        public int RoomCount => _roomCount;
    }
}
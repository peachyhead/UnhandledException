// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using Features.Room.Data;
using UnityEngine;

namespace Features.Level.Data.Configs
{
    [Serializable]
    public class LevelGenerationData
    {
        [SerializeField] private string _id;
        [SerializeField] private Vector3Int _bounds;
        [SerializeField] private List<RoomGenerationData> _roomData;

        public string ID => _id;
        public Vector3Int Bounds => _bounds;
        public List<RoomGenerationData> RoomData => _roomData;
    }
}
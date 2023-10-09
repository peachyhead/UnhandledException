// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using Base.Data;
using UnityEngine;

namespace Features.Room.Data.Config
{
    [CreateAssetMenu(fileName = "RoomViewRegistry", menuName = "Registry/Room view registry")]
    public class RoomViewRegistry : BaseConfig
    {
        [SerializeField] private List<RoomData> _roomData;

        public RoomData GetDataByID(RoomID id)
        {
            return _roomData.FirstOrDefault(data => data.RoomID == id);
        }
    }
}
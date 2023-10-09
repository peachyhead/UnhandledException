// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Level.Data;
using Features.Room.Data;
using Features.Room.Data.Config;
using Features.Room.Storages;
using Features.Utils;
using UnityEngine;

namespace Features.Room.Services
{
    public class RoomService
    {
        private readonly RoomModelStorage _roomModelStorage;
        private readonly RoomViewRegistry _roomViewRegistry;

        public RoomService(RoomModelStorage roomModelStorage,
            RoomViewRegistry roomViewRegistry)
        {
            _roomModelStorage = roomModelStorage;
            _roomViewRegistry = roomViewRegistry;
        }

        public void CreateRoom(RoomID roomID, Vector3Int bounds)
        {
            var place = FindSpace(bounds);
            var data = _roomViewRegistry.GetDataByID(roomID);
            var room = _roomModelStorage.GetNewRoom(data, place);
            room.SetPosition(place * GenerationConsts.GridSize);
        }

        private Vector3Int FindSpace(Vector3Int bounds)
        {
            var available = bounds.GetAvailable(_roomModelStorage.GetPlaces());
            return available.GetRandom();
        }
    }
}
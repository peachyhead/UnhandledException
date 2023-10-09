// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using System.Linq;
using Features.Room.Data;
using Features.Room.Factories;
using Features.Room.Models;
using Features.Utils;
using UniRx;
using UnityEngine;

namespace Features.Room.Storages
{
    public class RoomModelStorage
    {
        private readonly Dictionary<Vector3Int, RoomModel> _items = new ();
        private readonly RoomModelFactory _roomModelFactory;

        private readonly Subject<RoomModel> _roomCreationSubject = new ();
        private readonly Subject<RoomModel> _roomRemoveSubject = new ();

        private RoomModelStorage(RoomModelFactory roomModelFactory)
        {
            _roomModelFactory = roomModelFactory;
        }

        public RoomModel GetNewRoom(RoomData data, Vector3Int place)
        {
            var room = _roomModelFactory.Create(SystemUtils.GetGuidID(), data);
            _items.Add(place, room);

            _roomCreationSubject.OnNext(room);
            return room;
        }

        public void RemoveRoom(Vector3Int place)
        {
            if (!_items.TryGetValue(place, out var room)) return;
            _roomRemoveSubject.OnNext(room);
            room?.Dispose();
        }

        public List<Vector3Int> GetPlaces() => _items.Keys.ToList();

        public List<RoomModel> GetRooms() => _items.Values.ToList(); 
        
        public IObservable<RoomModel> OnRoomCreated()
        {
            return _roomCreationSubject.AsObservable();
        }

        public IObservable<RoomModel> OnRoomRemoved()
        {
            return _roomRemoveSubject.AsObservable();
        }
    }
}
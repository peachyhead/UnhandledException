// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using Features.Room.Factories;
using Features.Room.Models;
using Features.Room.Views;
using UniRx;
using Zenject;

namespace Features.Room.Storages
{
    public class RoomViewStorage : IInitializable, IDisposable
    {
        private readonly Dictionary<RoomModel, RoomPresenter> _rooms = new ();
        
        private readonly RoomModelStorage _modelStorage;
        private readonly RoomPresenterFactory _presenterFactory;

        private IDisposable _creationStream;
        private IDisposable _removeStream;

        private RoomViewStorage(RoomModelStorage modelStorage, 
            RoomPresenterFactory presenterFactory)
        {
            _modelStorage = modelStorage;
            _presenterFactory = presenterFactory;
        }

        public void Initialize()
        {
            foreach (var model in _modelStorage.GetRooms())
            {
                CreateRoom(model);
            }

            _creationStream = _modelStorage
                .OnRoomCreated()
                .Subscribe(CreateRoom);

            _removeStream = _modelStorage
                .OnRoomRemoved()
                .Subscribe(RemoveRoom);
        }

        private void CreateRoom(RoomModel model)
        {
            var presenter = _presenterFactory.Create(model);
            _rooms.TryAdd(model, presenter);
        }

        private void RemoveRoom(RoomModel model)
        {
            if (_rooms.ContainsKey(model));
                _rooms.Remove(model);
        }

        public void Dispose()
        {
            _rooms.Clear();
            _creationStream?.Dispose();
            _removeStream?.Dispose();
        }
    }
}
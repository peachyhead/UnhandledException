// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Room.Data;
using UniRx;
using UnityEngine;

namespace Features.Room.Models
{
    public class RoomModel : IDisposable
    {
        public readonly string ID;
        public readonly RoomData Data;
        public Vector3 Position => _positionProperty.Value;

        private readonly ReactiveProperty<Vector3> _positionProperty = new ();
        
        private readonly Subject<Unit> _disposeSubject = new ();

        public RoomModel(string id, RoomData data)
        {
            ID = id;
            Data = data;
        }

        public void SetPosition(Vector3 position)
        {
            _positionProperty.Value = position;
        }

        public IObservable<Vector3> OnPositionChange()
        {
            return _positionProperty.AsObservable();
        }

        public IObservable<Unit> OnDispose()
        {
            return _disposeSubject.AsObservable();
        }

        public void Dispose()
        {
            _positionProperty?.Dispose();
            _disposeSubject?.OnNext(new Unit());
        }
    }
}
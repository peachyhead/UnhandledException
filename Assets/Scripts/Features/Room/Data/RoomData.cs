// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Room.Views;
using UnityEngine;

namespace Features.Room.Data
{
    [Serializable]
    public class RoomData
    {
        [SerializeField] private RoomID _roomID;
        [SerializeField] private RoomPresenter _presenterPrefab;

        public RoomID RoomID => _roomID;
        public RoomPresenter PresenterPrefab => _presenterPrefab;
    }
}
// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Room.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Room.Views
{
    public class RoomPresenter : MonoBehaviour
    {
        private RoomModel _roomModel;

        [Inject]
        public void Construct(RoomModel roomModel)
        {
            _roomModel = roomModel;
        }

        private void Start()
        {
            _roomModel
                .OnPositionChange()
                .Subscribe(position => transform.position = position)
                .AddTo(this);

            _roomModel
                .OnDispose()
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
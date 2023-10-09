// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Room.Models;
using Features.Room.Views;
using Zenject;

namespace Features.Room.Factories
{
    public class RoomPresenterFactory : IFactory<RoomModel, RoomPresenter>
    {
        private readonly DiContainer _container;

        private RoomPresenterFactory(DiContainer container)
        {
            _container = container;
        }
        
        public RoomPresenter Create(RoomModel model)
        {
            return _container.InstantiatePrefabForComponent<RoomPresenter>(model.Data.PresenterPrefab,
                new object[] { model });
        }
    }
}
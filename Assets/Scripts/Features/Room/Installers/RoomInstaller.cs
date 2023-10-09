// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Room.Data;
using Features.Room.Factories;
using Features.Room.Models;
using Features.Room.Services;
using Features.Room.Storages;
using UnityEngine;
using Zenject;

namespace Features.Room.Installers
{
    public class RoomInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallFactories();
            InstallServices();
            InstallStorages();
        }

        private void InstallFactories()
        {
            Container.BindFactory<string, RoomData, RoomModel, RoomModelFactory>();
            Container.Bind<RoomPresenterFactory>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<RoomService>().AsSingle();
        }

        private void InstallStorages()
        {
            Container.Bind<RoomModelStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<RoomViewStorage>().AsSingle();
        }
    }
}
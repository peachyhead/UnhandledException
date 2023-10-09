// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Stages.Models;
using Zenject;

namespace Features.Stages.Installers
{
    public class StagesInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GenerationStage>().AsSingle();
        }
    }
}
// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Linq;
using Features.Stages.Data;
using Features.Stages.Factories;
using Features.Stages.Interfaces;
using Features.Stages.Models;
using Features.Stages.Rules;
using Features.Stages.Services;
using Zenject;

namespace Features.Stages.Installers
{
    public class StagesInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallFactories();
            InstallRules();
            InstallServices();
            InstallStages();
        }

        private void InstallFactories()
        {
            Container.Bind<StageFactory>().AsSingle();
        }

        private void InstallStages()
        {
            BindStageByType<GameStage>(StageType.Game);
            BindStageByType<LoadingStage>(StageType.Loading);
            BindStageByType<GenerationStage>(StageType.Generation);
        }

        private void InstallServices()
        {
            Container.Bind<StageService>().AsSingle();
        }

        private void InstallRules()
        {
            Container.BindInterfacesTo<StageExecuteRule>().AsSingle();
        }
        
        private void BindStageByType<TStage>(StageType type) where TStage : IStage
        {
            var interfaces = typeof(TStage).GetInterfaces();
            Container.Bind(interfaces.Prepend(typeof(TStage)))
                .WithId(type)
                .To(typeof(TStage))
                .AsSingle();
        }
    }
}
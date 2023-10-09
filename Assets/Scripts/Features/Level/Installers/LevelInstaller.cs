// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Level.Rules;
using Features.Level.Services;
using Features.Level.Signals;
using Zenject;

namespace Features.Level.Installers
{
    public class LevelInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallRules();
            InstallServices();
            InstallSignals();
        }

        private void InstallRules()
        {
            Container.BindInterfacesTo<LevelGenerationRule>().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<LevelGenerationService>().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<LevelSignals.GenerateLevel>();
        }
    }
}
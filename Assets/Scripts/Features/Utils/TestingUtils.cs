// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Bootstrap;
using Zenject;

namespace Features.Utils
{
    public static class TestingUtils
    {
        public static void InstallTestConfigs(this DiContainer container)
        {
            ConfigInstaller.InstallFromResource("Installers/ConfigInstaller_Test", container);
        }
    }
}
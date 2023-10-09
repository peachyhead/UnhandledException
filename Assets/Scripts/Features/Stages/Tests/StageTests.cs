// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections;
using System.Linq;
using Features.Stages.Data.Configs;
using Features.Stages.Installers;
using Features.Stages.Services;
using Features.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;
using Zenject;

namespace Features.Stages.Tests
{
    [TestFixture]
    public class StageTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            SignalBusInstaller.Install(Container);
            Container.Install<StagesInstaller>();
            Container.InstallTestConfigs();
        }

        [Test]
        public void Stage_Execute_Test()
        {
            var stageService = Container.Resolve<StageService>();
            var registry = Container.Resolve<StageRegistry>();
            stageService.SetupStage(registry.Stages.GetRandom());
            
            Assert.NotNull(stageService.CurrentStage);
        }
        
        [UnityTest]
        public IEnumerator Stage_Order_Test()
        {
            var stageService = Container.Resolve<StageService>();
            var registry = Container.Resolve<StageRegistry>();

            foreach (var awaiter in registry.Stages
                         .Select(stage => stageService.SetupStage(stage)))
            {
                while (!awaiter.IsCompleted)
                    yield return null;
            }
        }
    }
}
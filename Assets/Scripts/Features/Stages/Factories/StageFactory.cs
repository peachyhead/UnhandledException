// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Stages.Data;
using Features.Stages.Interfaces;
using Zenject;

namespace Features.Stages.Factories
{
    public class StageFactory : IFactory<StageType, IStage[], IStage>
    {
        private readonly DiContainer _container;

        public StageFactory(DiContainer container)
        {
            _container = container;
        }

        public IStage Create(StageType type, params IStage[] stages)
        {
            return _container.ResolveId<IStage>(type);
        }
    }
}
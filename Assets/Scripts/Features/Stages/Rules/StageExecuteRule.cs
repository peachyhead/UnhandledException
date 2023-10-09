// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Stages.Data.Configs;
using Features.Stages.Services;
using Zenject;

namespace Features.Stages.Rules
{
    public class StageExecuteRule : IInitializable
    {
        private readonly StageRegistry _stageRegistry;
        private readonly StageService _stageService;

        private StageExecuteRule(StageRegistry stageRegistry,
            StageService stageService)
        {
            _stageRegistry = stageRegistry;
            _stageService = stageService;
        }
        
        public void Initialize()
        {
            foreach (var data in _stageRegistry.Stages)
            {
                _stageService.SetupStage(data);
            }
        }
    }
}
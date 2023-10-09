// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Linq;
using Cysharp.Threading.Tasks;
using Features.Stages.Data.Configs;
using Features.Stages.Factories;
using Features.Stages.Interfaces;

namespace Features.Stages.Services
{
    public class StageService
    {
        public IStage CurrentStage;

        private readonly StageFactory _stageFactory;

        private StageService(StageFactory stageFactory)
        {
            _stageFactory = stageFactory;
        }
        
        public UniTask.Awaiter SetupStage(StageData stageData)
        {
            var childs = stageData.ChildStages
                .Select(childData => _stageFactory
                    .Create(childData.Type)).ToList();

            CurrentStage = _stageFactory.Create(stageData.Type);
            
            CurrentStage.SetContext(childs.ToArray());
            return CurrentStage.Execute().GetAwaiter();
        }
    }
}
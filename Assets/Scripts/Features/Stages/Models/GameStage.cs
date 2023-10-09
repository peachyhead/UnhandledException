// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Cysharp.Threading.Tasks;
using Features.Stages.Interfaces;
using UnityEngine;

namespace Features.Stages.Models
{
    public class GameStage : IStage
    {
        private IStage[] _stages;

        public void SetContext(params IStage[] stages)
        {
            _stages = stages;
        }

        public async UniTask Execute()
        {
            Debug.Log($"{GetType().Name} is Executing!");
            foreach (var stage in _stages)
            {
                var executionAwaiter = stage.Execute().GetAwaiter();
                await UniTask.WaitUntil(() => executionAwaiter.IsCompleted);
            }
        }
    }
}
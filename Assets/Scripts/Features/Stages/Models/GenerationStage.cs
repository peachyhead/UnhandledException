// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Cysharp.Threading.Tasks;
using Features.Level.Signals;
using Features.Stages.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Stages.Models
{
    public class GenerationStage : IStage
    {
        private readonly SignalBus _signalBus;

        public GenerationStage(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SetContext(params IStage[] stages)
        { }

        public UniTask Execute()
        {
            Debug.Log($"{GetType().Name} is Executing!");
            _signalBus.TryFire(new LevelSignals.GenerateLevel("past"));
            return UniTask.CompletedTask;
        }
    }
}
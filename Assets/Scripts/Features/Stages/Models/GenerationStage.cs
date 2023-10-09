// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Level.Signals;
using Zenject;

namespace Features.Stages.Models
{
    public class GenerationStage : IInitializable
    {
        private readonly SignalBus _signalBus;

        public GenerationStage(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.TryFire(new LevelSignals.GenerateLevel("past"));
        }
    }
}
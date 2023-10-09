// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Level.Data.Configs;
using Features.Level.Services;
using Features.Level.Signals;
using Features.Room.Storages;
using UniRx;
using Zenject;

namespace Features.Level.Rules
{
    public class LevelGenerationRule : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly LevelRegistry _levelRegistry;
        private readonly LevelGenerationService _generationService;

        private readonly CompositeDisposable _compositeDisposable = new ();

        private LevelGenerationRule(LevelRegistry levelRegistry, 
            LevelGenerationService generationService,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _levelRegistry = levelRegistry;
            _generationService = generationService;
        }
        
        public void Initialize()
        {
            _signalBus
                .GetStream<LevelSignals.GenerateLevel>()
                .Subscribe(signal =>
                {
                    var data = _levelRegistry.GetDataByID(signal.LevelID);
                    _generationService.GenerateLevel(data);
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
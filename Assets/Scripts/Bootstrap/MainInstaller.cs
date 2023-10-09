using System.Collections.Generic;
using Features.Level.Installers;
using Features.Room.Installers;
using Features.Stages.Installers;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(fileName = "MainInstaller", menuName = "Installer/MainInstaller")]
    public class MainInstaller : ScriptableObjectInstaller
    {
        [Header("Mandatory")] 
        [SerializeField] private List<GameObject> _mandatories;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Install<LevelInstaller>();
            Container.Install<StagesInstaller>();
            Container.Install<RoomInstaller>();
            
            foreach (var gameObject in _mandatories)
            {
                Container.BindInstance(gameObject).AsSingle();
            }
        }
    }
}

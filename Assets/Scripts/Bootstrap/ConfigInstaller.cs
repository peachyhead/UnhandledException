using System;
using System.Collections.Generic;
using Base.Data;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installer/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private List<BaseConfig> _configs;

        public override void InstallBindings()
        {
            foreach (var config in _configs)
            {
                InstallConfig(config.GetType(), config);
            }
        }
        
        private void InstallConfig(Type contractType, ScriptableObject config)
        {
            Container.BindInterfacesAndSelfTo(contractType)
                .FromScriptableObject(config)
                .AsSingle();
        }
    }
}

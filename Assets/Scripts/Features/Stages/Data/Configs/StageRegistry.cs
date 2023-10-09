// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using Base.Data;
using UnityEngine;

namespace Features.Stages.Data.Configs
{
    [CreateAssetMenu(fileName = "StageRegistry", menuName = "Registry/Stage registry")]
    public class StageRegistry : BaseConfig
    {
        [SerializeField] private List<StageData> _stages;
        
        public List<StageData> Stages => _stages;
    }
}
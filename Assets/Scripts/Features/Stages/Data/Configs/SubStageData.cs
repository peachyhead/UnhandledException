// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using UnityEngine;

namespace Features.Stages.Data.Configs
{
    [Serializable]
    public class SubStageData
    {
        [SerializeField] private StageType _type;
        
        public StageType Type => _type;
    }
}
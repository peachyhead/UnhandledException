// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Stages.Data.Configs
{
    [Serializable]
    public class StageData : SubStageData
    {
        [SerializeField] private List<SubStageData> _childStages;
        
        public List<SubStageData> ChildStages => _childStages;
    }
}
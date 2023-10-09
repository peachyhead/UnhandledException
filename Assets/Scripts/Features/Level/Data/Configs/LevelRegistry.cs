// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using Base.Data;
using UnityEngine;

namespace Features.Level.Data.Configs
{
    [CreateAssetMenu(fileName = "LevelRegistry", menuName = "Registry/Level registry")]
    public class LevelRegistry : BaseConfig
    {
        [SerializeField] private List<LevelGenerationData> _levelData;

        public LevelGenerationData GetDataByID(string id)
        {
            return _levelData.FirstOrDefault(data => data.ID == id);
        }
    }
}
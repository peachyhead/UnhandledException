// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

namespace Features.Level.Signals
{
    public sealed class LevelSignals
    {
        public class GenerateLevel
        {
            public readonly string LevelID;

            public GenerateLevel(string levelID)
            {
                LevelID = levelID;
            }
        }
    }
}
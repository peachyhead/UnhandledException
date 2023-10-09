// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Cysharp.Threading.Tasks;

namespace Features.Stages.Interfaces
{
    public interface IStage
    {
        public void SetContext(params IStage[] stages);
        public UniTask Execute();
    }
}
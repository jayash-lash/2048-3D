using UnityEngine;

namespace Services.Score.Abstraction
{
    public interface IScorePopupService
    {
        void Show(int score, Vector3 worldPosition);
    }
}
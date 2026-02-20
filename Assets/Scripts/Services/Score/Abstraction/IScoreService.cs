using System;

namespace Services.Score.Abstraction
{
    public interface IScoreService
    {
        int CurrentScore { get; }
        int BestScore { get; }

        event Action<int> OnScoreChanged;
        event Action<int> OnBestScoreChanged;

        void AddScore(int value);
        void ResetScore();
    }
}
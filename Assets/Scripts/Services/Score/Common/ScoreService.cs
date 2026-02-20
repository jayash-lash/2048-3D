using System;
using Services.Score.Abstraction;
using UnityEngine;

namespace Services.Score.Common
{
    public class ScoreService : IScoreService
    {
        private const string BestScoreKey = "BestScore";

        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }

        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;

        public ScoreService()
        {
            BestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        }

        public void AddScore(int value)
        {
            if (value <= 0)
                return;

            CurrentScore += value;
            OnScoreChanged?.Invoke(CurrentScore);

            if (CurrentScore > BestScore)
                UpdateBestScore(CurrentScore);
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            OnScoreChanged?.Invoke(CurrentScore);
        }

        private void UpdateBestScore(int value)
        {
            BestScore = value;
            PlayerPrefs.SetInt(BestScoreKey, BestScore);
            PlayerPrefs.Save();
            OnBestScoreChanged?.Invoke(BestScore);
        }
    }
}
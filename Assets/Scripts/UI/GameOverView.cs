using System;
using Services.Board.Abstraction;
using Services.Score.Abstraction;
using StateMachine.Abstraction;
using StateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _finalScoreLabel;
        [SerializeField] private TextMeshProUGUI _bestScoreLabel;
        [SerializeField] private Button _restartButton;

        private IScoreService _scoreService;
        private IBoardService _boardService;
        private IGameStateMachine _stateMachine;

        [Inject]
        private void Construct(IScoreService scoreService, IBoardService boardService, IGameStateMachine stateMachine)
        {
            _scoreService = scoreService;
            _boardService = boardService;
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _panel.SetActive(false);
            _restartButton.onClick.AddListener(OnRestartClicked);
            GameOverState.OnGameOver += Show;
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
            GameOverState.OnGameOver -= Show;
        }

        private void Show()
        {
            _panel.SetActive(true);
            _finalScoreLabel.text = $"Current Score: {_scoreService.CurrentScore.ToString()}";
            _bestScoreLabel.text = $"Best Score: {_scoreService.BestScore.ToString()}";
        }

        private void OnRestartClicked()
        {
            _panel.SetActive(false);
            _boardService.Reset();
            _scoreService.ResetScore();
            _stateMachine.Enter<SpawnState>();
        }
    }
}
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
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;
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

        private void Awake()
        {
            gameObject.SetActive(false);

            MainMenuState.OnMainMenu += Hide;
            GameOverState.OnGameOver += Hide;
            SpawnState.OnSpawn += Show;
        }

        private void Start()
        {
            UpdateScore(_scoreService.CurrentScore);
            UpdateBestScore(_scoreService.BestScore);

            _scoreService.OnScoreChanged += UpdateScore;
            _scoreService.OnBestScoreChanged += UpdateBestScore;
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDestroy()
        {
            _scoreService.OnScoreChanged -= UpdateScore;
            _scoreService.OnBestScoreChanged -= UpdateBestScore;
            _restartButton.onClick.RemoveListener(OnRestartClicked);

            MainMenuState.OnMainMenu -= Hide;
            GameOverState.OnGameOver -= Hide;
            SpawnState.OnSpawn -= Show;
        }

        private void Show() => gameObject.SetActive(true);
        private void Hide() => gameObject.SetActive(false);

        private void UpdateScore(int value) => _scoreLabel.text = value.ToString();
        private void UpdateBestScore(int value) => _bestScoreLabel.text = value.ToString();

        private void OnRestartClicked()
        {
            _boardService.Reset();
            _scoreService.ResetScore();
            _stateMachine.Enter<SpawnState>();
        }
    }
}
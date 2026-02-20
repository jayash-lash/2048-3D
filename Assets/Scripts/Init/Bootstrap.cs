using ScriptableObjects;
using Services.Board.Abstraction;
using Services.Pool.Abstractions;
using StateMachine.Abstraction;
using StateMachine.States;
using UnityEngine;
using Zenject;

namespace Init
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Inject] private readonly IPoolService _poolService;
        [Inject] private readonly IGameStateMachine _gameStateMachine;
        [Inject] private readonly CubeConfig _cubeConfig;
        [Inject] private readonly VFXConfig _vfxConfig;
        [Inject] private readonly IBoardService _boardService;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        
        private void Start()
        {
            InitializePools();
            _boardService.OnGameOver += OnGameOver;
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void InitializePools()
        {
            _poolService.CreatePool(_cubeConfig.CubePrefab, _cubeConfig.PoolStartSize, _cubeConfig.PoolIncreaseSizeBy);

            _poolService.CreatePool(_vfxConfig.MergeVFXPrefab, _vfxConfig.MergeVFXPoolSize);

            _poolService.CreatePool(_vfxConfig.SpawnVFXPrefab, _vfxConfig.SpawnVFXPoolSize);

            _poolService.CreatePool(_vfxConfig.ScorePopupPrefab, _vfxConfig.ScorePopupPoolSize);
        }

        private void OnGameOver() => _gameStateMachine.Enter<GameOverState>();

        private void OnDestroy() => _boardService.OnGameOver -= OnGameOver;
    }
}
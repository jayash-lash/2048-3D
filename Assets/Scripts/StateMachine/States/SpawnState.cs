using System;
using Cube.Launcher.Abstraction;
using ScriptableObjects;
using Services.Board.Abstraction;
using Services.Factory.Abstraction;
using Services.VFX.Abstraction;
using StateMachine.Abstraction;
using UnityEngine;

namespace StateMachine.States
{
    public class SpawnState : IState
    {
        public static event Action OnSpawn;

        private readonly IGameStateMachine _stateMachine;
        private readonly ICubeFactory _cubeFactory;
        private readonly IBoardService _boardService;
        private readonly ICubeLauncher _cubeLauncher;
        private readonly BoardConfig _boardConfig;
        private readonly CubeConfig _cubeConfig;
        private readonly IVFXService _vfxService;

        public SpawnState(IGameStateMachine stateMachine, ICubeFactory cubeFactory, IBoardService boardService, ICubeLauncher cubeLauncher, BoardConfig boardConfig, CubeConfig cubeConfig, IVFXService vfxService)
        {
            _stateMachine = stateMachine;
            _cubeFactory = cubeFactory;
            _boardService = boardService;
            _cubeLauncher = cubeLauncher;
            _boardConfig = boardConfig;
            _cubeConfig = cubeConfig;
            _vfxService = vfxService;
        }

        public void Enter()
        {
            if (_boardService.IsGameOver())
            {
                _stateMachine.Enter<GameOverState>();
                return;
            }

            OnSpawn?.Invoke();

            var spawnPosition = new Vector3(0f, _boardConfig.SpawnPositionY, _boardConfig.SpawnPositionZ);
            var value = _cubeConfig.GetRandomSpawnValue();

            var cube = _cubeFactory.Spawn(spawnPosition, value);
            _cubeLauncher.SetActiveCube(cube);
            _vfxService.PlaySpawnVFX(spawnPosition);
            _stateMachine.Enter<IdleState>();
        }

        public void Exit() { }
    }
}
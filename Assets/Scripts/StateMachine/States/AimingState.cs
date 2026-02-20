using Cube.Launcher.Abstraction;
using ScriptableObjects;
using Services.Input.Abstraction;
using StateMachine.Abstraction;
using UnityEngine;

namespace StateMachine.States
{
    public class AimingState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly ICubeLauncher _cubeLauncher;
        private readonly BoardConfig _boardConfig;

        public AimingState(IGameStateMachine stateMachine, IInputService inputService, ICubeLauncher launchController, BoardConfig boardConfig)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _cubeLauncher = launchController;
            _boardConfig = boardConfig;
        }

        public void Enter()
        {
            _inputService.OnFingerDrag += OnFingerDrag;
            _inputService.OnFingerUp += OnFingerUp;
        }

        public void Exit()
        {
            _inputService.OnFingerDrag -= OnFingerDrag;
            _inputService.OnFingerUp -= OnFingerUp;
        }

        private void OnFingerDrag(Vector2 delta)
        {
            float deltaX = delta.x * _boardConfig.DragSensitivity;
            _cubeLauncher.MoveCubeHorizontal(deltaX);
        }

        private void OnFingerUp(Vector2 position)
        {
            _inputService.Disable();
            _cubeLauncher.Launch();
            _stateMachine.Enter<LaunchedState>();
        }
    }
}
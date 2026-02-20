using Services.Input.Abstraction;
using StateMachine.Abstraction;

namespace StateMachine.States
{
    public class IdleState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;

        public IdleState(IGameStateMachine stateMachine, IInputService inputService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.Enable();
            _inputService.OnFingerDown += OnFingerDown;
        }

        public void Exit()
        {
            _inputService.OnFingerDown -= OnFingerDown;
        }

        private void OnFingerDown(UnityEngine.Vector2 position)
        {
            _stateMachine.Enter<AimingState>();
        }
    }
}
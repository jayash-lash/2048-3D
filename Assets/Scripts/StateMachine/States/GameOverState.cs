using System;
using Services.Input.Abstraction;
using StateMachine.Abstraction;

namespace StateMachine.States
{
    public class GameOverState : IState
    {
        private readonly IInputService _inputService;

        public static event Action OnGameOver;

        public GameOverState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.Disable();
            OnGameOver?.Invoke();
        }
        

        public void Exit() { }
    }
}
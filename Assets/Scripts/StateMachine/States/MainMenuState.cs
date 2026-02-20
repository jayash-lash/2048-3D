using System;
using StateMachine.Abstraction;

namespace StateMachine.States
{
    public class MainMenuState : IState
    {
        public static event Action OnMainMenu;

        public void Enter()
        {
            OnMainMenu?.Invoke();
        }

        public void Exit()
        {
        }
    }
}
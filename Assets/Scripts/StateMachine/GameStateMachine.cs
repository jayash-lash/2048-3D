using System;
using System.Collections.Generic;
using StateMachine.Abstraction;
using Zenject;

namespace StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly DiContainer _container;
        private readonly Dictionary<Type, IState> _stateCache = new();

        private IState _currentState;

        public GameStateMachine(DiContainer container)
        {
            _container = container;
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = GetOrCreateState<TState>();

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        private IState GetOrCreateState<TState>() where TState : class, IState
        {
            var type = typeof(TState);

            if (!_stateCache.TryGetValue(type, out var state))
            {
                state = _container.Resolve<TState>();
                _stateCache[type] = state;
            }

            return state;
        }
    }
}
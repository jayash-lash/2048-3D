using System.Threading;
using Cysharp.Threading.Tasks;
using ScriptableObjects;
using StateMachine.Abstraction;

namespace StateMachine.States
{
    public class LaunchedState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly BoardConfig _boardConfig;

        private CancellationTokenSource _cts;

        public LaunchedState(IGameStateMachine stateMachine, BoardConfig boardConfig)
        {
            _stateMachine = stateMachine;
            _boardConfig = boardConfig;
        }

        public void Enter()
        {
            _cts = new CancellationTokenSource();
            WaitForCubeToStopAsync(_cts.Token).Forget();
        }

        public void Exit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid WaitForCubeToStopAsync(CancellationToken token)
        {
            await UniTask.Delay((int)(_boardConfig.CubeStopCheckDelay * 1000), cancellationToken: token);
            await UniTask.Delay(300, cancellationToken: token);

            _stateMachine.Enter<SpawnState>();
        }
    }
}
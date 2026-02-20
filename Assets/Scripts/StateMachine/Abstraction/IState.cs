namespace StateMachine.Abstraction
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}
namespace Code.Infrastructure.States
{
  public interface IGameStateMachine
  {
    void RegisterState<TState>(TState state) where TState : class, IExitableState;
    void Enter<TState>() where TState : class, IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
  }
}
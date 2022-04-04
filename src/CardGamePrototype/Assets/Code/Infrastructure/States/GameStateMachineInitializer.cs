using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameStateMachineInitializer : IInitializable
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly LoadMenuState _loadMenuState;
    private readonly LoadLevelState _loadLevelState;
    private readonly GameLoopState _gameLoopState;
    private readonly GameOverState _gameOverState;
    private readonly LoadProgressState _loadProgressState;

    public GameStateMachineInitializer(IGameStateMachine stateMachine, LoadProgressState loadProgressState,
      LoadMenuState loadMenuState, LoadLevelState loadLevelState, GameLoopState gameLoopState,
      GameOverState gameOverState)
    {
      _stateMachine = stateMachine;
      _loadProgressState = loadProgressState;
      _loadMenuState = loadMenuState;
      _loadLevelState = loadLevelState;
      _gameLoopState = gameLoopState;
      _gameOverState = gameOverState;
    }

    public void Initialize()
    {
      RegisterStates();
      EnterLoadProgressState();
    }

    private void RegisterStates()
    {
      _stateMachine.RegisterState(_loadProgressState);
      _stateMachine.RegisterState(_loadMenuState);
      _stateMachine.RegisterState(_loadLevelState);
      _stateMachine.RegisterState(_gameLoopState);
      _stateMachine.RegisterState(_gameOverState);
    }

    private void EnterLoadProgressState() =>
      _stateMachine.Enter<LoadProgressState>();
  }
}
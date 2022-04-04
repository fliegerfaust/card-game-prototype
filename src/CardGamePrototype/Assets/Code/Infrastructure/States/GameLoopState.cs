using Code.Infrastructure.Signals.Game;
using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameLoopState : IState
  {
    private const string GameOverSceneName = "EndGame";

    private readonly SignalBus _signalBus;
    private readonly IGameStateMachine _stateMachine;

    public GameLoopState(SignalBus signalBus, IGameStateMachine stateMachine)
    {
      _signalBus = signalBus;
      _stateMachine = stateMachine;
    }

    public void Enter() =>
      _signalBus.Subscribe<HeroCardDeadSignal>(OnHeroDead);

    private void OnHeroDead() =>
      _stateMachine.Enter<GameOverState, string>(GameOverSceneName);

    public void Exit() =>
      _signalBus.Unsubscribe<HeroCardDeadSignal>(OnHeroDead);
  }
}
using Code.Infrastructure.Signals.Menu;
using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class LoadMenuState : IPayloadedState<string>
  {
    private const string GameSceneName = "Game";

    private readonly SceneLoader _sceneLoader;
    private readonly GameStateMachine _stateMachine;
    private readonly SignalBus _signalBus;

    public LoadMenuState(SceneLoader sceneLoader, GameStateMachine stateMachine, SignalBus signalBus)
    {
      _sceneLoader = sceneLoader;
      _stateMachine = stateMachine;
      _signalBus = signalBus;
    }

    public void Enter(string sceneName) =>
      _sceneLoader.Load(sceneName, OnLoaded);

    private void OnLoaded() =>
      _signalBus.Subscribe<StartButtonPressedSignal>(OnStartButtonPressed);

    private void OnStartButtonPressed() =>
      _stateMachine.Enter<LoadLevelState, string>(GameSceneName);

    public void Exit() =>
      _signalBus.Unsubscribe<StartButtonPressedSignal>(OnStartButtonPressed);
  }
}
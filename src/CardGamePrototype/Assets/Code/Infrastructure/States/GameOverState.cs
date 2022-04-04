using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Game;
using Code.Infrastructure.Signals.EndGame;
using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameOverState : IPayloadedState<string>
  {
    private readonly SceneLoader _sceneLoader;
    private readonly PersistentProgressService _progressService;
    private readonly DifficultyDataService _difficultyService;
    private readonly SignalBus _signalBus;
    private readonly IGameStateMachine _stateMachine;
    private readonly SaveLoadService _saveLoadService;

    public GameOverState(SceneLoader sceneLoader, PersistentProgressService progressService,
      DifficultyDataService difficultyService, SignalBus signalBus, IGameStateMachine stateMachine,
      SaveLoadService saveLoadService)
    {
      _sceneLoader = sceneLoader;
      _progressService = progressService;
      _difficultyService = difficultyService;
      _signalBus = signalBus;
      _stateMachine = stateMachine;
      _saveLoadService = saveLoadService;
    }

    public void Enter(string sceneKey)
    {
      _sceneLoader.Load(sceneKey, OnLoaded);
      _signalBus.Subscribe<RestartButtonPressedSignal>(StartNewGame);
    }

    private void StartNewGame() =>
      _stateMachine.Enter<LoadProgressState>();

    private void OnLoaded()
    {
      UpdateProgress();
      _saveLoadService.SaveProgress();
      _signalBus.Fire<ProgressSavedSignal>();
    }

    private void UpdateProgress()
    {
      _progressService.Progress.Points = _difficultyService.Points;
      _progressService.Progress.DifficultyLevel = _difficultyService.DifficultyLevel;

      if (_progressService.Progress.Points > _progressService.Progress.BestPoints)
        _progressService.Progress.BestPoints = _progressService.Progress.Points;
    }

    public void Exit()
    {
      _signalBus.Unsubscribe<RestartButtonPressedSignal>(StartNewGame);
      _difficultyService.ResetScore();
    }
  }
}
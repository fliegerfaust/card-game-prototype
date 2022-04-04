using Code.Data;
using Code.Infrastructure.Services;
using JetBrains.Annotations;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class LoadProgressState : IState
  {
    private const string MenuSceneName = "Menu";

    private readonly GameStateMachine _stateMachine;
    private readonly PersistentProgressService _progressService;
    private readonly SaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine stateMachine, PersistentProgressService progressService,
      SaveLoadService saveLoadService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();
      _stateMachine.Enter<LoadMenuState, string>(MenuSceneName);
    }

    private void LoadProgressOrInitNew() =>
      _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

    private PlayerProgress NewProgress()
    {
      PlayerProgress progress = new PlayerProgress(0, 1, 0);
      return progress;
    }

    public void Exit()
    {
    }
  }
}
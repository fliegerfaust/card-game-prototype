using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.StaticData;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly SceneLoader _sceneLoader;
    private readonly IStaticDataService _staticData;
    private readonly IGameFactory _gameFactory;
    private readonly IGameStateMachine _stateMachine;

    public LoadLevelState(SceneLoader sceneLoader, IStaticDataService staticData, IGameFactory gameFactory,
      IGameStateMachine stateMachine)
    {
      _sceneLoader = sceneLoader;
      _staticData = staticData;
      _gameFactory = gameFactory;
      _stateMachine = stateMachine;
    }

    public void Enter(string sceneName) =>
      _sceneLoader.Load(sceneName, OnLoaded);

    private void OnLoaded()
    {
      LoadData();
      InitGameWorld();
      StartGame();
    }

    private void StartGame() =>
      _stateMachine.Enter<GameLoopState>();

    private void LoadData() =>
      _staticData.Load();

    private void InitGameWorld()
    {
      LevelStaticData levelStaticData = LevelStaticData();
      InitSpawners(levelStaticData);
    }

    private void InitSpawners(LevelStaticData levelData)
    {
      foreach (CardSlotData slotData in levelData.CardSlots)
        _gameFactory.CreateSpawner(slotData.Position, slotData.CardSpawnId);
    }

    public void Exit()
    {
    }

    private LevelStaticData LevelStaticData() =>
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
  }
}
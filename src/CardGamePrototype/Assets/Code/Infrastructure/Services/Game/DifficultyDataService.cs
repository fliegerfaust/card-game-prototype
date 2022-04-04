using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Signals.Game;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Services.Game
{
  [UsedImplicitly]
  public class DifficultyDataService
  {
    public int Points { get; private set; }
    public int DifficultyLevel { get; private set; }

    private readonly SignalBus _signalBus;
    private readonly LevelStaticData _levelData;

    public DifficultyDataService(SignalBus signalBus, IStaticDataService staticData)
    {
      _signalBus = signalBus;

      staticData.Load();
      _levelData = staticData.ForLevel(SceneManager.GetActiveScene().name);

      ResetScore();
    }

    public void IncreasePoints()
    {
      Points += _levelData.MoveCost;

      _signalBus.Fire(new PointsChangedSignal() {Value = Points});

      if (Points % _levelData.DifficultyLevelCost == 0)
      {
        DifficultyLevel += 1;
        _signalBus.Fire(new DifficultyChangedSignal() {Value = DifficultyLevel});
      }
    }

    public void ResetScore()
    {
      DifficultyLevel = 1;
      Points = 0;
    }
  }
}
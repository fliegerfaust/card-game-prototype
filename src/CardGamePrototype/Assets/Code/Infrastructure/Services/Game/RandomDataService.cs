using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.Game
{
  [UsedImplicitly]
  public class RandomDataService
  {
    private const float ChanceMultiplier = 0.1f;
    private const float MaxRedCardChance = 0.5f;

    public CardSpawnId CardSpawnId =>
      Random.value <= RedCardChance() ? CardSpawnId.Enemy : CardSpawnId.Heal;

    public int CardValue => (int) Random.Range(0f, _difficultyData.DifficultyLevel + 1) + 1;

    private readonly DifficultyDataService _difficultyData;

    public RandomDataService(DifficultyDataService difficultyData) =>
      _difficultyData = difficultyData;

    private float RedCardChance()
    {
      float currentChance = _difficultyData.DifficultyLevel * ChanceMultiplier;
      currentChance = currentChance > MaxRedCardChance ? MaxRedCardChance : currentChance;
      return currentChance;
    }
  }
}
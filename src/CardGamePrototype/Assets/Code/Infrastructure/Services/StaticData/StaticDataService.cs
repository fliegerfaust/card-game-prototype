using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
  [UsedImplicitly]
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataCardsPath = "StaticData/Cards";
    private const string StaticDataLevelPath = "StaticData/LevelData";

    private LevelStaticData _level;
    private Dictionary<CardSpawnId, CardStaticData> _cards;

    public void Load()
    {
      LoadLevel();
      LoadCards();
    }

    public LevelStaticData ForLevel(string levelKey) =>
      _level;

    public CardStaticData ForCard(CardSpawnId typeId) =>
      _cards.TryGetValue(typeId, out CardStaticData staticData) ? staticData : null;

    private void LoadLevel() =>
      _level = Resources.Load<LevelStaticData>(StaticDataLevelPath);

    private void LoadCards() =>
      _cards = Resources.LoadAll<CardStaticData>(StaticDataCardsPath)
        .ToDictionary(x => x.CardSpawnId, x => x);
  }
}
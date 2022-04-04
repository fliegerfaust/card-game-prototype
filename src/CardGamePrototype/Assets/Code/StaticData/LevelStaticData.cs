using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Levels", order = 0)]
  public class LevelStaticData : ScriptableObject
  {
    public string SceneKey;
    public int MoveCost = 1;
    public int DifficultyLevelCost = 10;
    public List<CardSlotData> CardSlots;
  }
}
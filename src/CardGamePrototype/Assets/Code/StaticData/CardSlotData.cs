using System;
using UnityEngine;

namespace Code.StaticData
{
  [Serializable]
  public class CardSlotData
  {
    public CardSpawnId CardSpawnId;
    public Vector3 Position;

    public CardSlotData(CardSpawnId cardSpawnId, Vector3 position)
    {
      CardSpawnId = cardSpawnId;
      Position = position;
    }
  }
}
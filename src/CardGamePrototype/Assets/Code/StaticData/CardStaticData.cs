using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "CardData", menuName = "StaticData/Cards", order = 0)]
  public class CardStaticData : ScriptableObject
  {
    public CardSpawnId CardSpawnId;
    [Range(1, 100)] public int InitialCardValue;
    public GameObject Prefab;
  }
}
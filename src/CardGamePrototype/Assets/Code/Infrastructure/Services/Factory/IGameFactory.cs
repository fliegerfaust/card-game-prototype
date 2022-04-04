using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Services.Factory
{
  public interface IGameFactory
  {
    GameObject CreateCard(CardSpawnId cardSpawnId, Transform parent, bool useRandomService);
    void CreateSpawner(Vector3 at, CardSpawnId cardSpawnId);
  }
}
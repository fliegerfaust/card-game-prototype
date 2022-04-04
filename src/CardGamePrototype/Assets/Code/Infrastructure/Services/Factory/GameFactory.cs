using Code.Cards;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Logic.CardSlots;
using Code.Infrastructure.Services.Game;
using Code.Infrastructure.Services.StaticData;
using Code.StaticData;
using Code.UI.Game;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.Factory
{
  [UsedImplicitly]
  public class GameFactory : IGameFactory
  {
    private readonly DiContainer _diContainer;
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly RandomDataService _randomData;

    public GameFactory(DiContainer diContainer, IAssets assets, IStaticDataService staticData,
      RandomDataService randomData)
    {
      _diContainer = diContainer;
      _assets = assets;
      _staticData = staticData;
      _randomData = randomData;
    }

    public void CreateSpawner(Vector3 at, CardSpawnId cardSpawnId)
    {
      GameObject prefab = _assets.Instantiate(AssetPath.SpawnerPath, at);
      _diContainer.InjectGameObject(prefab);

      SpawnPoint spawner = prefab.GetComponent<SpawnPoint>();
      spawner.CardSpawnId = cardSpawnId;
    }

    public GameObject CreateCard(CardSpawnId cardSpawnId, Transform parent, bool useRandomService)
    {
      cardSpawnId = useRandomService ? _randomData.CardSpawnId : cardSpawnId;

      CardStaticData cardData = _staticData.ForCard(cardSpawnId);
      GameObject card = Object.Instantiate(cardData.Prefab, parent.position, Quaternion.identity, parent);

      IHealth health = card.GetComponent<IHealth>();
      health.Current = useRandomService ? _randomData.CardValue : cardData.InitialCardValue;

      CardMarker cardMarker = card.GetComponent<CardMarker>();
      cardMarker.CardType = cardSpawnId;

      card.GetComponent<CardView>().Init(health);

      _diContainer.BindInterfacesAndSelfTo<IHealth>().FromInstance(health);
      _diContainer.InjectGameObject(card);

      return card;
    }
  }
}
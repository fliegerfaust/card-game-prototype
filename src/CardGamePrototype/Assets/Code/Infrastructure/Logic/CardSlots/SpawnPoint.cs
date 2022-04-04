using Code.Infrastructure.Services.Factory;
using Code.StaticData;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Logic.CardSlots
{
  public class SpawnPoint : MonoBehaviour
  {
    public CardSpawnId CardSpawnId;
    private Sequence _sequence;

    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IGameFactory gameFactory) =>
      _gameFactory = gameFactory;

    private void Start() =>
      Spawn();

    private void Spawn()
    {
      GameObject card = _gameFactory.CreateCard(CardSpawnId, transform, false);
      SpawnAnimation(card.transform);
    }

    public void Spawn(CardSpawnId cardSpawnId, Transform parent)
    {
      GameObject card = _gameFactory.CreateCard(cardSpawnId, parent, true);
      SpawnAnimation(card.transform);
    }

    private void SpawnAnimation(Transform card)
    {
      _sequence = DOTween.Sequence();
      _sequence.Append(card.DOScale(0.5f, .5f)).SetEase(Ease.OutQuart);
      _sequence.Append(card.DOScale(0.3f, .5f)).SetEase(Ease.OutQuart);
    }
  }
}
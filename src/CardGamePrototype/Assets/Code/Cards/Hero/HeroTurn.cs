using System.Collections;
using Code.Infrastructure.Logic.CardSlots;
using Code.Infrastructure.Services.Game;
using Code.Infrastructure.Services.Input;
using Code.StaticData;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Cards.Hero
{
  public class HeroTurn : MonoBehaviour
  {
    private const float MaxMoveDistance = 2.5f;

    private IHealth _heroHealth;
    private Camera _mainCamera;

    private IInputService _inputService;
    private DifficultyDataService _difficultyDataService;
    private Sequence _sequence;

    [Inject]
    public void Construct(IInputService inputService, DifficultyDataService difficultyDataService)
    {
      _inputService = inputService;
      _difficultyDataService = difficultyDataService;

      _inputService.OnClick += DoTurn;
    }

    private void Start()
    {
      _mainCamera = Camera.main;
      _heroHealth = GetComponent<IHealth>();
    }

    private void DoTurn(Vector3 position)
    {
      Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(position);
      RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

      if (hit.collider != null)
      {
        Transform selectedCard = hit.collider.transform;
        Vector3 heroPosition = transform.position;

        if (CheckDistance(heroPosition, selectedCard.position))
        {
          hit.collider.TryGetComponent(out CardMarker cardMarker);
          hit.collider.TryGetComponent(out IHealth cardValue);

          switch (cardMarker.CardType)
          {
            case CardSpawnId.Enemy:
              _heroHealth.TakeDamage(cardValue.Current);
              break;
            case CardSpawnId.Heal:
              _heroHealth.TakeHeal(cardValue.Current);
              break;
          }

          Transform currentSlot = transform.parent;

          MoveAnimation(to: selectedCard);
          ChangeCurrentSlot(to: selectedCard);
          StartCoroutine(SpawnNewCard(currentSlot: currentSlot));

          _difficultyDataService.IncreasePoints();
        }
      }
    }

    private void MoveAnimation(Transform to)
    {
      _sequence = DOTween.Sequence();
      _sequence.Append(to.DOScale(0f, .5f)).SetEase(Ease.OutQuart).AppendInterval(.3f);
      to.gameObject.SetActive(false);
      _sequence.Append(transform.DOMove(to.position, .5f)).SetEase(Ease.OutQuart);
    }

    private void ChangeCurrentSlot(Transform to)
    {
      Transform newSlot = to.parent.transform;
      transform.SetParent(newSlot);

      Destroy(to.gameObject, 1f);
    }

    private IEnumerator SpawnNewCard(Transform currentSlot)
    {
      yield return new WaitForSeconds(1f);

      SpawnPoint spawnPoint = GetComponentInParent<SpawnPoint>();
      spawnPoint.Spawn(CardSpawnId.Enemy, currentSlot);
    }

    private static bool CheckDistance(Vector3 heroPosition, Vector3 selectedCardPosition) =>
      Vector3.Distance(heroPosition, selectedCardPosition) <= MaxMoveDistance;

    private void OnDestroy()
    {
      _inputService.OnClick -= DoTurn;
      _sequence.Kill();
    }
  }
}
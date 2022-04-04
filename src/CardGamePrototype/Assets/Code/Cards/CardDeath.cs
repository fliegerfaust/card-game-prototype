using System.Collections;
using Code.Infrastructure.Signals.Game;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Cards
{
  public class CardDeath : MonoBehaviour
  {
    [SerializeField] private Health _health;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
      _signalBus = signalBus;
      _signalBus.Subscribe<CardValueChangedSignal>(CardHealthChanged);
    }

    private void CardHealthChanged()
    {
      if (_health.Current <= 0)
        Die();
    }

    private void Die()
    {
      _signalBus.Unsubscribe<CardValueChangedSignal>(CardHealthChanged);
      DeathAnimation();
      Destroy(gameObject, 2f);
      StartCoroutine(FireEvent());
    }

    private IEnumerator FireEvent()
    {
      yield return new WaitForSeconds(1f);

      _signalBus.Fire<HeroCardDeadSignal>();
    }

    private void DeathAnimation()
    {
      Sequence sequence = DOTween.Sequence();
      sequence.Append(transform.DOScale(0f, .5f)).SetEase(Ease.OutQuart);
      sequence.Kill();
    }
  }
}
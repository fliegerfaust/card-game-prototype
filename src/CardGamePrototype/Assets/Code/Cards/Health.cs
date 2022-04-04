using Code.Infrastructure.Signals.Game;
using UnityEngine;
using Zenject;

namespace Code.Cards
{
  public class Health : MonoBehaviour, IHealth
  {
    [SerializeField] private int _current;

    public int Current
    {
      get => _current;
      set => _current = value;
    }

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) =>
      _signalBus = signalBus;

    public void TakeHeal(int value)
    {
      Current += value;
      _signalBus.Fire(new CardValueChangedSignal() {Value = Current});
    }

    public void TakeDamage(int value)
    {
      Current -= value;
      _signalBus.Fire(new CardValueChangedSignal() {Value = Current});
    }
  }
}
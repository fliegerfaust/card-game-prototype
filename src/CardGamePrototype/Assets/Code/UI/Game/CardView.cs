using Code.Cards;
using Code.Infrastructure.Signals.Game;
using Code.StaticData;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Game
{
  public class CardView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _cardValueText;
    private SignalBus _signalBus;
    private CardSpawnId _cardSpawnId;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
      _signalBus = signalBus;
      _signalBus.Subscribe<CardValueChangedSignal>(UpdateValue);
    }

    public void Init(IHealth health)
    {
      UpdateText(health.Current);
      _cardSpawnId = GetComponent<CardMarker>().CardType;
    }

    private void UpdateValue(CardValueChangedSignal signalInfo)
    {
      if (_cardSpawnId == CardSpawnId.Hero)
        UpdateText(signalInfo.Value);
    }

    private void UpdateText(int value)
    {
      value = value < 0 ? 0 : value;
      _cardValueText.SetText(value.ToString());
    }

    private void OnDestroy() =>
      _signalBus.Unsubscribe<CardValueChangedSignal>(UpdateValue);
  }
}
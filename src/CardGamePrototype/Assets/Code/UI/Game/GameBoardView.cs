using Code.Infrastructure.Signals.Game;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Game
{
  public class GameBoardView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _difficultyText;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
      _signalBus = signalBus;
      _signalBus.Subscribe<PointsChangedSignal>(OnPointsChanged);
      _signalBus.Subscribe<DifficultyChangedSignal>(OnDifficultyChanged);
    }

    private void OnPointsChanged(PointsChangedSignal signal) =>
      _pointsText.SetText($"Points: {signal.Value}");

    private void OnDifficultyChanged(DifficultyChangedSignal signal) =>
      _difficultyText.SetText($"Difficulty: {signal.Value}");

    private void OnDestroy()
    {
      _signalBus.Unsubscribe<PointsChangedSignal>(OnPointsChanged);
      _signalBus.Unsubscribe<DifficultyChangedSignal>(OnDifficultyChanged);
    }
  }
}
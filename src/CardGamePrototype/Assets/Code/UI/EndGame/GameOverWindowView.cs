using Code.Data;
using Code.Infrastructure.Services;
using Code.Infrastructure.Signals.EndGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.EndGame
{
  public class GameOverWindowView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _difficultyLevelText;
    [SerializeField] private Button _restartButton;

    private SignalBus _signalBus;
    private SaveLoadService _saveLoadService;

    [Inject]
    public void Construct(SignalBus signalBus, SaveLoadService saveLoadService)
    {
      _signalBus = signalBus;
      _saveLoadService = saveLoadService;

      _restartButton.onClick.AddListener(OnRestartButtonPressed);
      _signalBus.Subscribe<ProgressSavedSignal>(PrintProgress);
    }

    private void OnRestartButtonPressed() =>
      _signalBus.Fire<RestartButtonPressedSignal>();

    private void PrintProgress()
    {
      PlayerProgress playerProgress = _saveLoadService.LoadProgress();

      _pointsText.SetText($"Your points: {playerProgress.Points}");
      _difficultyLevelText.SetText($"Difficulty level: {playerProgress.DifficultyLevel}");
    }

    private void OnDestroy() =>
      _signalBus.Unsubscribe<ProgressSavedSignal>(PrintProgress);
  }
}
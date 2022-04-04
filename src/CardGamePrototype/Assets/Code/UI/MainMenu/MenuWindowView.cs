using Code.Infrastructure.Services;
using Code.Infrastructure.Signals.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.MainMenu
{
  public class MenuWindowView : MonoBehaviour
  {
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _recordText;

    private SignalBus _signalBus;
    private PersistentProgressService _progressService;

    [Inject]
    public void Construct(SignalBus signalBus, PersistentProgressService progressService)
    {
      _signalBus = signalBus;
      _progressService = progressService;
      _startButton.onClick.AddListener(OnStartButtonPressed);
      _exitButton.onClick.AddListener(OnExitButtonPressed);
    }

    private void Start() =>
      _recordText.SetText($"Best Points Record: {_progressService.Progress.BestPoints}");

    private void OnStartButtonPressed() =>
      _signalBus.Fire<StartButtonPressedSignal>();

    private void OnExitButtonPressed() =>
      Application.Quit();
  }
}
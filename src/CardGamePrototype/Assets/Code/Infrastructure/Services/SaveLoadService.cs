using Code.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services
{
  [UsedImplicitly]
  public class SaveLoadService
  {
    private const string ProgressKey = "Progress";

    private readonly PersistentProgressService _progressService;

    public SaveLoadService(PersistentProgressService progressService) =>
      _progressService = progressService;

    public void SaveProgress()
    {
      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
      PlayerPrefs.Save();
    }

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
  }
}
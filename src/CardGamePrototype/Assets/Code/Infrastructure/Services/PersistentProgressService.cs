using Code.Data;
using JetBrains.Annotations;

namespace Code.Infrastructure.Services
{
  [UsedImplicitly]
  public class PersistentProgressService
  {
    public PlayerProgress Progress { get; set; }
  }
}
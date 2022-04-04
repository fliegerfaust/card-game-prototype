using Code.StaticData;

namespace Code.Infrastructure.Services.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    LevelStaticData ForLevel(string levelKey);
    CardStaticData ForCard(CardSpawnId typeId);
  }
}
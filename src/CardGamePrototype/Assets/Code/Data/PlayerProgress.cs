using System;

namespace Code.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public int Points;
    public int DifficultyLevel;
    public int BestPoints;

    public PlayerProgress(int points, int difficultyLevel, int bestPoints)
    {
      Points = points;
      DifficultyLevel = difficultyLevel;
      BestPoints = bestPoints;
    }
  }
}
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  [UsedImplicitly]
  public class AssetProvider : IAssets
  {
    public GameObject Instantiate(string path, Vector2 at)
    {
      GameObject prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }
  }
}
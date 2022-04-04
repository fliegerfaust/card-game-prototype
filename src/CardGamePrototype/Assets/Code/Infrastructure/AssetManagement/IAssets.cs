using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssets
  {
    GameObject Instantiate(string path, Vector2 at);
  }
}
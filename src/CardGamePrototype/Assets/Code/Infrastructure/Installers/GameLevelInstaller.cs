using Code.UI.Game;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class GameLevelInstaller : MonoInstaller
  {
    [SerializeField] private GameBoardView _gameBoardView;

    public override void InstallBindings() =>
      BindGameBoardView();

    private void BindGameBoardView() =>
      Container.Bind<GameBoardView>().FromInstance(_gameBoardView);
  }
}
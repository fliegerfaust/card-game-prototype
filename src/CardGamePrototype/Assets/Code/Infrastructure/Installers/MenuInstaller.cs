using Code.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class MenuInstaller : MonoInstaller
  {
    [SerializeField] private MenuWindowView _menuWindow;

    public override void InstallBindings() =>
      Container.Bind<MenuWindowView>().FromInstance(_menuWindow);
  }
}
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.Game;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Signals.EndGame;
using Code.Infrastructure.Signals.Game;
using Code.Infrastructure.Signals.Menu;
using Code.Infrastructure.States;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
  {
    public override void InstallBindings()
    {
      BindCoroutineRunner();
      BindSceneLoader();

      BindInputService();
      BindStaticDataService();
      BindAssetProvider();
      BindDifficultyService();
      BindGameRandomService();
      BindGameFactory();
      BindProgressService();
      BindSaveLoadService();

      DeclareZenjectSignals();

      BindGameStates();
      BindGameStateMachine();
    }

    private void DeclareZenjectSignals()
    {
      SignalBusInstaller.Install(Container);
      Container.DeclareSignal<StartButtonPressedSignal>();
      Container.DeclareSignal<CardValueChangedSignal>();
      Container.DeclareSignal<PointsChangedSignal>();
      Container.DeclareSignal<DifficultyChangedSignal>();
      Container.DeclareSignal<HeroCardDeadSignal>();
      Container.DeclareSignal<ProgressSavedSignal>();
      Container.DeclareSignal<RestartButtonPressedSignal>();
    }

    private void BindCoroutineRunner() =>
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindSceneLoader() =>
      Container.Bind<SceneLoader>().AsSingle();

    private void BindInputService() =>
      Container.BindInterfacesAndSelfTo<MouseInput>().AsSingle();

    private void BindStaticDataService() =>
      Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindAssetProvider() =>
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();

    private void BindDifficultyService() =>
      Container.BindInterfacesAndSelfTo<DifficultyDataService>().AsSingle();

    private void BindGameRandomService() =>
      Container.BindInterfacesAndSelfTo<RandomDataService>().AsSingle();

    private void BindGameFactory() =>
      Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();

    private void BindProgressService() =>
      Container.BindInterfacesAndSelfTo<PersistentProgressService>().AsSingle();

    private void BindSaveLoadService() =>
      Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsTransient();
      Container.BindInterfacesAndSelfTo<LoadMenuState>().AsTransient();
      Container.BindInterfacesAndSelfTo<LoadLevelState>().AsTransient();
      Container.BindInterfacesAndSelfTo<GameLoopState>().AsTransient();
      Container.BindInterfacesAndSelfTo<GameOverState>().AsTransient();
    }

    private void BindGameStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameStateMachineInitializer>().AsSingle();
    }
  }
}
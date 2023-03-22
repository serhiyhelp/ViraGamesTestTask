using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services;
using Services.CompareObjectListsService;
using Services.Input;
using Services.ObjectGrouper;
using Services.ObjectMover;
using Services.WindowService;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      RegisterStaticData();

      _services.RegisterSingle<IInputService>(InputService());
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IObjectGrouper>(new ObjectGrouper());
      _services.RegisterSingle<IObjectMover>(new ObjectMover());
      _services.RegisterSingle<IResetGameService>(new ResetGameService(_stateMachine));
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
      _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IStaticDataService>(),_services.Single<IObjectMover>(), _services.Single<IResetGameService>()));
      _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
      _services.RegisterSingle<ICompareObjectListsService>(new CompareObjectListsService(_services.Single<IObjectMover>(), _services.Single<IWindowService>()));
    }

    private void RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadStaticData();
      _services.RegisterSingle(staticData);
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>("Game");
    
    private static IInputService InputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new MobileInputService();
    }
  }
}
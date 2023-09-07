using Windows;
using Infrastructure.Services;
using Services;
using Services.ObjectMover;
using Services.WindowService;
using StaticData;
using StaticData.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class UIFactory : IUIFactory 
    {
        private const string UIRootTag = "UIRoot";
        
        private readonly IStaticDataService _staticData;
        private readonly IObjectMover _objectMover;
        private readonly IResetGameService _resetGameService;

        private Transform _uiRoot;

        public UIFactory()
        {
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _objectMover = AllServices.Container.Single<IObjectMover>();
            _resetGameService = AllServices.Container.Single<IResetGameService>();
        }
        
        public void CreateStartScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowID.StartScreen);
            var startScreen = Object.Instantiate(config.Prefab, _uiRoot).GetComponent<StartScreen>();
            startScreen.InitStartScreen(_objectMover);
        }

        public void CreateDefeatScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowID.DefeatScreen);
            var defeatScreen = Object.Instantiate(config.Prefab, _uiRoot).GetComponent<DefeatScreen>();
            defeatScreen.InitDefeatScreen(_resetGameService);
        }

        public void CreateVictoryScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowID.VictoryScreen);
            var victoryScreen = Object.Instantiate(config.Prefab, _uiRoot).GetComponent<VictoryScreen>();
            victoryScreen.InitDefeatScreen(_resetGameService);
        }

        public void FindRootObject() => _uiRoot = GameObject.FindGameObjectWithTag(UIRootTag).transform;
    }
}
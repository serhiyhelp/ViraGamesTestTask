using Windows;
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

        public UIFactory(IStaticDataService staticDataService, IObjectMover objectMover,IResetGameService resetGameService)
        {
            _staticData = staticDataService;
            _objectMover = objectMover;
            _resetGameService = resetGameService;
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
using System;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.ObjectMover;

namespace Services.WindowService
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IObjectMover _objectMover;

        public WindowService()
        {
            _uiFactory = AllServices.Container.Single<IUIFactory>();
        }
        
        public void Open(WindowID windowID, Action windowAction = null)
        {
            _uiFactory.FindRootObject();
            
            switch (windowID)
            {
                case WindowID.Unknown:
                    break;
                case WindowID.StartScreen:
                    _uiFactory.CreateStartScreen();
                    break;
                case WindowID.VictoryScreen:
                    _uiFactory.CreateVictoryScreen();
                    break;
                case WindowID.DefeatScreen:
                    _uiFactory.CreateDefeatScreen();
                    break;
            }
        }
    }
}
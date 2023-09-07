using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Services.Firebase;
using Services.WindowService;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader      _sceneLoader;
        private readonly LoadingCurtain   _loadingCurtain;
        private readonly IGameFactory     _gameFactory;
        private readonly IWindowService   _windowService;
        private readonly FirebaseService  _firebaseService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _stateMachine    = gameStateMachine;
            _sceneLoader     = sceneLoader;
            _loadingCurtain  = loadingCurtain;
            _gameFactory     = services.Single<IGameFactory>();
            _windowService   = services.Single<IWindowService>();
            _firebaseService = services.Single<FirebaseService>();
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            _windowService.Open(WindowID.StartScreen);
            _gameFactory.CreatePlayerSpot(GameObject.FindWithTag(InitialPointTag));

            var levelId               = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);
            if (levelId == 0) levelId = 1;
            _firebaseService.LogLevelStart(levelId);
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
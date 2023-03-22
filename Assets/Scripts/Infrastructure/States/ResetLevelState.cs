using Logic;

namespace Infrastructure.States
{
    public class ResetLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public ResetLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }
        
        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Reload(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadLevelState, string>("Game");
        }

        public void Exit()
        {
            
        }
    }
}
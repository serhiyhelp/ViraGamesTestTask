using Infrastructure.States;

namespace Services
{
    public class ResetGameService : IResetGameService
    {
        private readonly GameStateMachine _stateMachine;

        public ResetGameService(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void ResetGame()
        {
            _stateMachine.Enter<ResetLevelState, string>("Game");
        }
    }
}
using Services;

namespace Windows
{
    public class DefeatScreen : WindowBase
    {
        private IResetGameService _resetGameService;

        public void InitDefeatScreen(IResetGameService resetGameService)
        {
            _resetGameService = resetGameService;
            
            CloseButton.onClick.AddListener(ResetButtonAction);
        }

        private void ResetButtonAction()
        {
            _resetGameService.ResetGame();
        }

    }
}
using Infrastructure.Services;

namespace Services
{
    public interface IResetGameService : IService
    {
        void ResetGame();
    }
}
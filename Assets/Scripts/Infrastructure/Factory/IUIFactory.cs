using Infrastructure.Services;

namespace Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        void CreateStartScreen();
        void FindRootObject();
        void CreateDefeatScreen();
        void CreateVictoryScreen();
    }
}
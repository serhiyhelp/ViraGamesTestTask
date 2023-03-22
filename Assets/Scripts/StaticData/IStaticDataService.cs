using Infrastructure.Services;
using Services.WindowService;
using StaticData.Windows;

namespace StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        LevelStaticData ForLevel(int levelIdKey);
        WindowConfig ForWindow(WindowID window);
    }
}
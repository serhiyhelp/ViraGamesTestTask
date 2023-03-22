using System.Collections.Generic;
using System.Linq;
using Services.WindowService;
using StaticData.Windows;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string ConfigsLevelStaticData = "Configs/LevelStaticData";
        private const string ConfigsWindowStaticData = "Configs/WindowStaticData/WindowStaticData";
        private Dictionary<int, LevelStaticData> _levels;
        private Dictionary<WindowID, WindowConfig> _windowConfigs;

        public void LoadStaticData()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(ConfigsLevelStaticData)
                .ToDictionary(x => x.levelIdKey, x => x);

            _windowConfigs = Resources.Load<WindowStaticData>(ConfigsWindowStaticData).Configs.ToDictionary(x => x.WindowID, x=> x);
        }

        public LevelStaticData ForLevel(int levelIdKey) => 
            _levels
                .TryGetValue(levelIdKey, out LevelStaticData staticData) 
                ? staticData 
                : null;
        
        public WindowConfig ForWindow(WindowID window) => 
            _windowConfigs
                .TryGetValue(window, out WindowConfig windowConfig)
                ? windowConfig 
                : null;

    }
}
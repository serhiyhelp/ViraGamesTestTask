using System;
using Windows;
using Services.WindowService;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowID WindowID;
        public WindowBase Prefab;
    }
}
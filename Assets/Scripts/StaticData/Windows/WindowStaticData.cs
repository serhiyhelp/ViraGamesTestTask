using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Configs/WindowStaticData", order = 0)]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}
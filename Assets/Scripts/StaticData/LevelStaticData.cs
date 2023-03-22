using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData" , menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        public int levelIdKey;
        public float timeToPlay;
            
        [Header("Enemy Settings")] 
        public int enemySpotsAmount;
        public Vector2Int enemyAmountBounds;
    
        [Header("Upgrade Wall Settings")]
        public int upgradeWallAmount;
        public Vector2Int upgradePlusAmountBounds;
        public Vector2Int upgradeMultiplyAmountBounds;
    }
}

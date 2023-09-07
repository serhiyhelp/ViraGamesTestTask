using Firebase.Analytics;
using Infrastructure.Services;
using UnityEngine;

namespace Services.Firebase
{
    public class FirebaseService : IService
    {
        public FirebaseService()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }
        
        public void LogLevelStart(int levelId)
        {
            Debug.Log($"Level {levelId} Start");
            FirebaseAnalytics.LogEvent("LevelStart", "LevelNumber", levelId);
        }
        
        public void LogLevelFail(int levelId)
        {
            Debug.Log($"Level {levelId} Fail");
            FirebaseAnalytics.LogEvent("LevelFail", "LevelNumber", levelId);
        }
        
        public void LogLevelComplete(int levelId)
        {
            Debug.Log($"Level {levelId} Complete ");
            FirebaseAnalytics.LogEvent("LevelComplete", "LevelNumber", levelId);
        }
        
    }
}
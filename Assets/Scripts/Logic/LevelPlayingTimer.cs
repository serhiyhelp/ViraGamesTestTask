using System.Collections;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using StaticData;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class LevelPlayingTimer : MonoBehaviour
    {
        public UnityEvent TimePassed;
        
        private IStaticDataService _staticData;

        private void Awake()
        {
            _staticData = AllServices.Container.Single<IStaticDataService>();
        }

        public void StartLevelPlayingCoroutine()
        {
            var levelId = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);
            if (levelId == 0) levelId = 1;
            var timeToPlay = _staticData.ForLevel(levelId).timeToPlay;

            IEnumerator Routine()
            {
                yield return new WaitForSecondsRealtime(timeToPlay);
                TimePassed.Invoke();
            }
            StartCoroutine(Routine());
        }
    }
}
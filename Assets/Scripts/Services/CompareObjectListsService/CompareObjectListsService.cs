using System.Collections;
using Enemy;
using Infrastructure.AssetManagement;
using Services.Firebase;
using Services.ObjectMover;
using Services.WindowService;
using UnityEngine;

namespace Services.CompareObjectListsService
{
    public class CompareObjectListsService : ICompareObjectListsService
    {
        private readonly IObjectMover    _objectMover;
        private readonly IWindowService  _windowService;
        private readonly FirebaseService _firebaseService;

        public CompareObjectListsService(IObjectMover objectMover, IWindowService windowService, FirebaseService firebaseService)
        {
            _objectMover     = objectMover;
            _windowService   = windowService;
            _firebaseService = firebaseService;
        }
        
        public IEnumerator CompareLists(EnemySpot enemy, Player.Player player)
        {
            _objectMover.MoveAction(false);

            var playerList = player.PlayerObjects;
            var enemyList  = enemy.EnemySpotObjects;

            var playerListCount = playerList.Count;
            var enemyListCount  = enemyList.Count;

            var timeBetweenDisable = enemyListCount <= 10 ? 0.1f : .03f;
            
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (playerListCount <= 0)
                {

                    var levelId = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);
                    if (levelId == 0) levelId = 1;
                    _firebaseService.LogLevelFail(levelId);
                    
                    _windowService.Open(WindowID.DefeatScreen);
                    player.gameObject.SetActive(false);
                    yield break;
                }
                
                playerList[i].gameObject.SetActive(false);
                enemyList[i].gameObject.SetActive(false);

                playerListCount--;
                enemyListCount--;

                enemy.UpdateEnemySpotCounter(enemyListCount);
                player.UpdatePlayerCounterValue(playerListCount);
                
                yield return new WaitForSeconds(timeBetweenDisable);
            }

            playerList.RemoveRange(0, enemyList.Count);
            enemyList.Clear();
            enemy.gameObject.SetActive(false);

            _objectMover.MoveAction(true);
        }
    }
}
using Services.CompareObjectListsService;
using UnityEngine;

namespace Enemy
{
    public class EnemySpotTrigger : MonoBehaviour
    {
        private ICompareObjectListsService _compareObjectListsService;
        private EnemySpot _enemySpot;

        public void InitEnemySpotTrigger(EnemySpot enemySpot,ICompareObjectListsService compareObjectListsService)
        {
            _enemySpot = enemySpot;
            _compareObjectListsService = compareObjectListsService;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                _enemySpot.SetupFightTarget(player.transform.position);
                StartCoroutine(_compareObjectListsService.CompareLists(_enemySpot, player));
            }
        }
    }
}
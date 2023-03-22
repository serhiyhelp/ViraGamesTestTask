using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.CompareObjectListsService;
using Services.ObjectGrouper;
using Services.ObjectMover;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class ObjSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnSpotTransform;
        [SerializeField] private GameObject enemySpawnSpotTransform;
    
        private IGameFactory _factory;
        private IStaticDataService _staticData;
        private IObjectGrouper _objectGrouper;
        private ICompareObjectListsService _compareService;
        private IObjectMover _objectMover;

        private LevelStaticData _levelData;
        private List<UpgradeWall.UpgradeWall> _upgradeWallPool = new List<UpgradeWall.UpgradeWall>();
        private List<EnemySpot> _enemySpotPool = new List<EnemySpot>();

        private void Awake()
        {
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _factory = AllServices.Container.Single<IGameFactory>();
            _objectGrouper = AllServices.Container.Single<IObjectGrouper>();
            _compareService = AllServices.Container.Single<ICompareObjectListsService>();
            _objectMover = AllServices.Container.Single<IObjectMover>();

            var levelData = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);

            if (levelData == 0)
            {
                levelData = 1;
            }
        
            _levelData = _staticData.ForLevel(levelData);
            _upgradeWallPool = SpawnUpgradeWall();
            _enemySpotPool = SpawnEnemySpot();

            StartCoroutine(SpawnUpgradeWallCoroutine());
            StartCoroutine(SpawnEnemySpotCoroutine());
        }

        private List<UpgradeWall.UpgradeWall> SpawnUpgradeWall()
        {
            List<UpgradeWall.UpgradeWall> newList = new List<UpgradeWall.UpgradeWall>();

            for (int i = 0; i < _levelData.upgradeWallAmount; i++)
            {
                var upgradeWall = _factory.CreateUpgradeWall(spawnSpotTransform).GetComponent<UpgradeWall.UpgradeWall>();
                upgradeWall.gameObject.SetActive(false);
                newList.Add(upgradeWall);
            }

            return newList;
        }

        private List<EnemySpot> SpawnEnemySpot()
        {
            List<EnemySpot> newList = new List<EnemySpot>();

            for (int i = 0; i < _levelData.enemySpotsAmount; i++)
            {
                var enemySpot = _factory.CreateEnemySpot(enemySpawnSpotTransform).GetComponent<EnemySpot>();
                enemySpot.gameObject.SetActive(false);
                newList.Add(enemySpot);
            }

            return newList;
        }

        private UpgradeWall.UpgradeWall RequestUpgradeWall()
        {
            foreach (var upgradeWall in _upgradeWallPool.Where(upgradeWall => !upgradeWall.gameObject.activeInHierarchy))
            {
                upgradeWall.transform.position = spawnSpotTransform.transform.position;
                return upgradeWall;
            }
        
            var newWall = _factory.CreateUpgradeWall(spawnSpotTransform).GetComponent<UpgradeWall.UpgradeWall>();
            newWall.gameObject.SetActive(true);
            _upgradeWallPool.Add(newWall);
            return newWall;
        }

        private EnemySpot RequestEnemySpot()
        {
            foreach (var enemySpot in _enemySpotPool.Where(enemySpot => !enemySpot.gameObject.activeInHierarchy))
            {
                enemySpot.transform.position = enemySpawnSpotTransform.transform.position;
                return enemySpot;
            }

            var newEnemySpot = _factory.CreateEnemySpot(enemySpawnSpotTransform).GetComponent<EnemySpot>();
        
            newEnemySpot.gameObject.SetActive(true);
            _enemySpotPool.Add(newEnemySpot);
            return newEnemySpot;
        }
    
        IEnumerator SpawnUpgradeWallCoroutine()
        {
            while (true)
            {
                var wall = RequestUpgradeWall();
                wall.InitUpgradeWall(_levelData, _objectMover);
                wall.gameObject.SetActive(true);
                yield return new WaitForSeconds(7f);
            }
        }

        IEnumerator SpawnEnemySpotCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                var enemySpot = RequestEnemySpot();
                enemySpot.InitEnemySpot(_levelData, _factory,_objectGrouper, _compareService, _objectMover);
                enemySpot.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                enemySpot.gameObject.SetActive(true);
            }
        }
    }
}

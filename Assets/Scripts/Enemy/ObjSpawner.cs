using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Services.CompareObjectListsService;
using Services.ObjectGrouper;
using Services.ObjectMover;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class ObjSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject        _spawnSpotTransform;
        [SerializeField] private GameObject        _enemySpawnSpotTransform;
        [SerializeField] private LevelPlayingTimer _timer;
        [SerializeField] private Transform         _endPoint;

        private IGameFactory               _factory;
        private IStaticDataService         _staticData;
        private IObjectGrouper             _objectGrouper;
        private ICompareObjectListsService _compareService;
        private IObjectMover               _objectMover;

        private LevelStaticData               _levelData;
        private List<UpgradeWall.UpgradeWall> _upgradeWallPool = new List<UpgradeWall.UpgradeWall>();
        private List<EnemySpot>               _enemySpotPool   = new List<EnemySpot>();

        private Coroutine _wallSpawnCoroutine;
        private Coroutine _enemySpawnCoroutine;

        private void Awake()
        {
            _staticData     = AllServices.Container.Single<IStaticDataService>();
            _factory        = AllServices.Container.Single<IGameFactory>();
            _objectGrouper  = AllServices.Container.Single<IObjectGrouper>();
            _compareService = AllServices.Container.Single<ICompareObjectListsService>();
            _objectMover    = AllServices.Container.Single<IObjectMover>();

            var levelData = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey);

            if (levelData == 0)
            {
                levelData = 1;
            }

            _levelData       = _staticData.ForLevel(levelData);
            _upgradeWallPool = CreateUpgradeWallPool();
            _enemySpotPool   = CreateEnemySpotPool();

        }

        private void Start()
        {
            _wallSpawnCoroutine  = StartCoroutine(SpawnUpgradeWallCoroutine());
            _enemySpawnCoroutine = StartCoroutine(SpawnEnemySpotCoroutine());
        }

        private void OnEnable()
        {
            _timer.TimePassed.AddListener(OnTimePassed);
        }

        private void OnDisable()
        {
            _timer.TimePassed.RemoveListener(OnTimePassed);
        }

        private void OnTimePassed()
        {
            StopCoroutine(_wallSpawnCoroutine);
            StopCoroutine(_enemySpawnCoroutine);
        }

        private IEnumerator SpawnUpgradeWallCoroutine()
        {
            while (true)
            {
                var wall = RequestUpgradeWall();
                wall.InitUpgradeWall(_levelData, _objectMover, _endPoint.transform.position.z);
                wall.gameObject.SetActive(true);
                yield return new WaitForSeconds(7f);
            }
        }

        private IEnumerator SpawnEnemySpotCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);

                var enemySpot = RequestEnemySpot();
                enemySpot.InitEnemySpot(_levelData, _factory, _objectGrouper, _compareService, _objectMover);
                enemySpot.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                enemySpot.gameObject.SetActive(true);
            }
        }

        private List<UpgradeWall.UpgradeWall> CreateUpgradeWallPool()
        {
            var newList = new List<UpgradeWall.UpgradeWall>();

            for (var i = 0; i < _levelData.upgradeWallAmount; i++)
            {
                var upgradeWall = _factory.CreateUpgradeWall(_spawnSpotTransform)
                                          .GetComponent<UpgradeWall.UpgradeWall>();
                upgradeWall.gameObject.SetActive(false);
                newList.Add(upgradeWall);
            }

            return newList;
        }

        private List<EnemySpot> CreateEnemySpotPool()
        {
            var newList = new List<EnemySpot>();

            for (var i = 0; i < _levelData.enemySpotsAmount; i++)
            {
                var enemySpot = _factory.CreateEnemySpot(_enemySpawnSpotTransform).GetComponent<EnemySpot>();
                enemySpot.gameObject.SetActive(false);
                newList.Add(enemySpot);
            }

            return newList;
        }

        private UpgradeWall.UpgradeWall RequestUpgradeWall()
        {
            var reusedWall = _upgradeWallPool.FirstOrDefault(w => w.gameObject.activeInHierarchy == false);

            if (reusedWall)
            {
                reusedWall.transform.position = _spawnSpotTransform.transform.position;
                return reusedWall;
            }

            var newWall = _factory.CreateUpgradeWall(_spawnSpotTransform).GetComponent<UpgradeWall.UpgradeWall>();
            newWall.gameObject.SetActive(true);
            _upgradeWallPool.Add(newWall);
            return newWall;
        }

        private EnemySpot RequestEnemySpot()
        {
            var reusedSpot = _enemySpotPool.FirstOrDefault(s => s.gameObject.activeInHierarchy == false);

            if (reusedSpot)
            {
                reusedSpot.transform.position = _enemySpawnSpotTransform.transform.position;
                return reusedSpot;
            }

            var newEnemySpot = _factory.CreateEnemySpot(_enemySpawnSpotTransform).GetComponent<EnemySpot>();

            newEnemySpot.gameObject.SetActive(true);
            _enemySpotPool.Add(newEnemySpot);
            return newEnemySpot;
        }
    }
}
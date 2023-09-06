using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Logic;
using Services.CompareObjectListsService;
using Services.ObjectGrouper;
using Services.ObjectMover;
using StaticData;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpot : MonoBehaviour
    {
        [SerializeField] private Counter _counter;

        private LevelStaticData _levelStaticData;
        private Vector3         _fightTarget;
        private bool            _isFighting;

        private IGameFactory               _gameFactory;
        private IObjectGrouper             _objectGrouper;
        private ICompareObjectListsService _compareObjectListsService;
        private IObjectMover               _objectMover;

        public List<Transform> EnemySpotObjects { get; } = new List<Transform>();

        public void InitEnemySpot(LevelStaticData            levelStaticData,
                                  IGameFactory               gameFactory,
                                  IObjectGrouper             objectGrouper,
                                  ICompareObjectListsService compareObjectListsService,
                                  IObjectMover               objectMover)
        {
            _objectMover               = objectMover;
            _compareObjectListsService = compareObjectListsService;
            _objectGrouper             = objectGrouper;
            _levelStaticData           = levelStaticData;
            _gameFactory               = gameFactory;

            GenerateEnemySpotObjects();

            UpdateEnemySpotCounter(EnemySpotObjects.Count);
            _isFighting = false;

            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                _isFighting  = true;
                _fightTarget = player.transform.position;
                StartCoroutine(_compareObjectListsService.CompareLists(this, player));
            }
        }

        public void UpdateEnemySpotCounter(int newAmount)
        {
            _counter.ChangeCounterValue(newAmount);
        }

        private void Update()
        {
            _objectMover.UpdateObjectPosition(transform, Vector3.forward);

            if (_isFighting)
            {
                _objectMover.MoveTowardsToObject(transform, _fightTarget);
            }
        }

        private void GenerateEnemySpotObjects()
        {
            var randomEnemyAmount = Random.Range(_levelStaticData.enemyAmountBounds.x, _levelStaticData.enemyAmountBounds.y + 1);
        
            var diffAmount = EnemySpotObjects.Count - randomEnemyAmount;
            if (diffAmount < 0)
                diffAmount = -diffAmount;
        
            for (var i = 0; i < diffAmount; i++)
            {
                var newEnemyObj = _gameFactory.CreateEnemyObject(gameObject);
                newEnemyObj.gameObject.SetActive(false);
                EnemySpotObjects.Add(newEnemyObj.transform);
            }
        
            ActivateEnemies(randomEnemyAmount);
        }

        private void ActivateEnemies(int randomEnemyAmount)
        {
            if(randomEnemyAmount > EnemySpotObjects.Count) return;
        
            for (int i = 0; i < randomEnemyAmount; i++)
            {
                EnemySpotObjects[i].gameObject.SetActive(true);
            }
        
            _objectGrouper.GroupObjects(EnemySpotObjects, .5f);
        }
    }
}
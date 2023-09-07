using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Services.Input;
using Services.ObjectGrouper;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Counter _counter;
        [Space]
        [SerializeField] private float _roadHalfWidth = 2.75f;
        [SerializeField] private float _speed = 1.2f;


        private IObjectGrouper      _objectGrouper;
        private PlayerMover         _playerMover;
        private IGameFactory        _gameFactory;
        private PlayerObjectSpawner _playerObjectSpawner;
        private SphereCollider      _sphereCollider;
        
        public PlayerObjectSpawner PlayerObjectSpawner => _playerObjectSpawner;
        public SphereCollider      SphereCollider      => _sphereCollider;

        public List<Transform> PlayerObjects { get; } = new List<Transform>();
        
        
        private void Awake()
        {
            _objectGrouper = AllServices.Container.Single<IObjectGrouper>();
            _gameFactory   = AllServices.Container.Single<IGameFactory>();
            
            _sphereCollider = gameObject.AddComponent<SphereCollider>();
            
            _playerMover         = new PlayerMover(_roadHalfWidth, _speed);
            _playerObjectSpawner = new PlayerObjectSpawner(this);
            
            _objectGrouper.GroupObjects(PlayerObjects, .5f);
            _objectGrouper.CalculateGroupColliderSize(PlayerObjects, _sphereCollider);
            
            var playerObj = _gameFactory.CreatePlayerObject(gameObject);
            PlayerObjects.Add(playerObj.transform);
            UpdatePlayerCounterValue(PlayerObjects.Count);
        }

        private void Update()
        {
#if UNITY_EDITOR
            _playerMover.UpdatePosStandalone(transform);
#else
            _playerMover.UpdatePosMobile(transform);
#endif
        }

        public void UpdatePlayerCounterValue(int newAmount)
        {
            _counter.ChangeCounterValue(newAmount);
        }
    }
}
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
        public List<Transform> playerObjects;

        [SerializeField] private Counter counter;
        
        private IInputService _inputService;
        private IObjectGrouper _objectGrouper;
        private PlayerMover _playerMover;
        private IGameFactory _gameFactory;
        private PlayerObjectSpawner _playerObjectSpawner;
        
        private SphereCollider _sphereCollider;
        public PlayerObjectSpawner PlayerObjectSpawner => _playerObjectSpawner;
        public SphereCollider SphereCollider => _sphereCollider;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _objectGrouper = AllServices.Container.Single<IObjectGrouper>();
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            
            _sphereCollider = gameObject.AddComponent<SphereCollider>();
            
            _playerMover = new PlayerMover(_inputService);
            _playerObjectSpawner = new PlayerObjectSpawner(this, _gameFactory, _objectGrouper);
            
            _objectGrouper.GroupObjects(playerObjects, .5f);
            _objectGrouper.CalculateGroupColliderSize(playerObjects, _sphereCollider);
            
            UpdatePlayerCounterValue(playerObjects.Count);
        }

        private void Update()
        {
#if UNITY_EDITOR
            _playerMover.UpdatePosStandalone(transform);
            return;
#endif
            _playerMover.UpdatePosMobile(transform);
        }

        public void UpdatePlayerCounterValue(int newAmount)
        {
            counter.ChangeCounterValue(newAmount);
        }
    }
}
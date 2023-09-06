using Infrastructure.Factory;
using Services.ObjectGrouper;

namespace Player
{
    public class PlayerObjectSpawner
    {
        private readonly Player _player;
        private readonly IGameFactory _gameFactory;
        private readonly IObjectGrouper _objectGrouper;

        public PlayerObjectSpawner(Player player,IGameFactory gameFactory, IObjectGrouper objectGrouper)
        {
            _player = player;
            _gameFactory = gameFactory;
            _objectGrouper = objectGrouper;
        }

        public void SpawnPlayerObject(int valueToAdd)
        {
            for (int i = 0; i < valueToAdd; i++)
            {
                var playerObj = _gameFactory.CreatePlayerObject(_player.gameObject);
                _player.PlayerObjects.Add(playerObj.transform);
                _player.UpdatePlayerCounterValue(_player.PlayerObjects.Count);
            }
            
            _objectGrouper.GroupObjects(_player.PlayerObjects, .5f);
            _objectGrouper.CalculateGroupColliderSize(_player.PlayerObjects,_player.SphereCollider);
        }
    }
}
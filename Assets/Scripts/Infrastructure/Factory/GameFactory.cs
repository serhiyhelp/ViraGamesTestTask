using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public GameObject CreatePlayerSpot(GameObject at) =>
      _assets.Instantiate(path: AssetPath.PlayerSpotPath, at: at.transform.position);

    public GameObject CreateEnemySpot(GameObject at) =>
      _assets.Instantiate(path: AssetPath.EnemySpotPath, at: at.transform.position);

    public GameObject CreatePlayerObject(GameObject at) => 
      _assets.Instantiate(path: AssetPath.PlayerObject, at.transform);

    public GameObject CreateEnemyObject(GameObject at) =>
      _assets.Instantiate(path: AssetPath.EnemyObject, at.transform);

    public GameObject CreateUpgradeWall(GameObject at) => 
      _assets.Instantiate(path: AssetPath.UpgradeWallPath, at.transform.position);

    public GameObject CreateFinishLine(Vector3 at) =>
      _assets.Instantiate(AssetPath.FinishLinePath, at);
  }
}
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
  public interface IGameFactory:IService
  {
    GameObject CreatePlayerSpot(GameObject at);
    GameObject CreateEnemySpot(GameObject at);
    GameObject CreatePlayerObject(GameObject at);
    GameObject CreateEnemyObject(GameObject at);
    GameObject CreateUpgradeWall(GameObject at);
    GameObject CreateFinishLine(Vector3 at);
  }
}
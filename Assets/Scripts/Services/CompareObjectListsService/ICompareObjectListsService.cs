using System.Collections;
using Enemy;
using Infrastructure.Services;

namespace Services.CompareObjectListsService
{
    public interface ICompareObjectListsService : IService
    {
        IEnumerator CompareLists(EnemySpot enemy, Player.Player player);
    }
}
using System.Collections.Generic;
using Services.ObjectMover;
using StaticData;
using UnityEngine;

namespace UpgradeWall
{
    public class UpgradeWall : MonoBehaviour
    {
        [SerializeField] private List<WallTrigger> _wallTriggers;

        private IObjectMover _objectMover;
        private float        _endPointZ;


        public void InitUpgradeWall(LevelStaticData levelStaticData, IObjectMover objectMover, float endPointZ)
        {
            _endPointZ   = endPointZ;
            _objectMover = objectMover;

            SetupWallTriggers(levelStaticData);
        }

        private void SetupWallTriggers(LevelStaticData levelStaticData)
        {
            var randomPlusValue = Random.Range(levelStaticData.upgradePlusAmountBounds.x,
                levelStaticData.upgradePlusAmountBounds.y + 1);
            var randomMultiplyValue = Random.Range(levelStaticData.upgradeMultiplyAmountBounds.x,
                levelStaticData.upgradeMultiplyAmountBounds.y + 1);
            var resultPlusValue = Mathf.RoundToInt(randomPlusValue / 5.0f) * 5;

            foreach (var trigger in _wallTriggers)
            {
                if (Random.Range(0, 2) == 0)
                    trigger.InitTrigger(WallType.PlusWall, resultPlusValue, DisableTriggers);
                else
                    trigger.InitTrigger(WallType.MultiplyWall, randomMultiplyValue, DisableTriggers);
            }
        }

        private void Update()
        {
            _objectMover.UpdateObjectPosition(transform, Vector3.back);

            if (transform.position.z < _endPointZ)
            {
                gameObject.SetActive(false);
            }
        }

        private void DisableTriggers()
        {
            foreach (var trigger in _wallTriggers) trigger.Disable();
        }
    }
}
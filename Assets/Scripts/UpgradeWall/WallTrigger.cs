using System;
using TMPro;
using UnityEngine;

namespace UpgradeWall
{
    public class WallTrigger : MonoBehaviour
    {
        [SerializeField] private Collider    _collider;
        [SerializeField] private GameObject  _hologram;
        [SerializeField] private WallType    _wallType;
        [SerializeField] private TextMeshPro _valueText;

        private int    _triggerValue;
        private Action _triggerAction;

        public void InitTrigger(WallType wallTypeValue, int value, Action triggerAction = null)
        {
            if (value > 0)
            {
                _wallType       = wallTypeValue;
                _triggerValue   = value;
                _valueText.text = (wallTypeValue == WallType.MultiplyWall ? "x" : "+") + value;

                _triggerAction = triggerAction;
                _hologram.SetActive(true);
                _collider.enabled = true;
            }
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                _triggerAction?.Invoke();
                _hologram.gameObject.SetActive(false);
                switch (_wallType)
                {
                    case WallType.PlusWall:
                        player.PlayerObjectSpawner.SpawnPlayerObject(_triggerValue);
                        break;
                    case WallType.MultiplyWall:
                        var multiplyValue = player.playerObjects.Count * _triggerValue;
                        player.PlayerObjectSpawner.SpawnPlayerObject(multiplyValue);
                        break;
                }
            }
        }
    }
}
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.Firebase;
using Services.ObjectMover;
using Services.WindowService;
using UnityEngine;

namespace Logic
{
    public class LevelFinisher : MonoBehaviour
    {
        [SerializeField] private LevelPlayingTimer _timer;

        [Space]
        [SerializeField] private Vector3 _finishLineSpawnPosition = new Vector3(0, 4.08f, 75f);

        private IGameFactory _factory;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
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
            var finishLine = _factory.CreateFinishLine(_finishLineSpawnPosition).GetComponent<FinishLine>();
            finishLine.InitFinishLine();
        }
    }
}
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
        
        private IGameFactory    _factory;
        private IWindowService  _windowService;
        private IObjectMover    _objectMover;
        private FirebaseService _firebaseService;

        private void Awake()
        {
            _factory         = AllServices.Container.Single<IGameFactory>();
            _windowService   = AllServices.Container.Single<IWindowService>();
            _objectMover     = AllServices.Container.Single<IObjectMover>();
            _firebaseService = AllServices.Container.Single<FirebaseService>();
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
            finishLine.InitFinishLine(_windowService, _objectMover, _firebaseService);
        }
    }
}
using Infrastructure.Services;
using Services.ObjectMover;
using UnityEngine;

namespace Logic
{
    public class MoverSpeed : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private void Start()
        {
            AllServices.Container.Single<IObjectMover>().Speed = _speed;
        }
    }
}
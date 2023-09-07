using Infrastructure.Services;
using UnityEngine;

namespace Services.ObjectMover
{
    public interface IObjectMover : IService
    {
        float Speed { get; set; }
        
        void UpdateObjectPosition(Transform t, Vector3 direction);
        void MoveAction(bool isAvailable);
        void MoveTowardsToObject(Transform t, Vector3 target);
    }
}
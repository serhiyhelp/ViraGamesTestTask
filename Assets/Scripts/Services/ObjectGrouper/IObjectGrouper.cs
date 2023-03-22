using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

namespace Services.ObjectGrouper
{
    public interface IObjectGrouper : IService
    {
        void GroupObjects(List<Transform> objectsToGroup, float groupingRadius);
        void CalculateGroupColliderSize(List<Transform> objectsToGroup, SphereCollider groupCollider);
    }
}
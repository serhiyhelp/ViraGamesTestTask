using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.ObjectGrouper
{
    public class ObjectGrouper : IObjectGrouper
    {
        public void GroupObjects(List<Transform> objectsToGroup, float groupingRadius)
        {
            foreach (var obj in objectsToGroup)
            {
                var nearbyObjects = GetNearbyObjects(obj, objectsToGroup, groupingRadius);
                var nearbyObjectsCount = nearbyObjects.Count;
                var angleBetweenObjects = 360.0f / nearbyObjectsCount;
                for (var i = 0; i < nearbyObjectsCount; i++)
                {
                    var angle = i * angleBetweenObjects;
                    var newPosition = obj.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * groupingRadius;
                    nearbyObjects[i].position = newPosition;
                }
            }
        }
    
        private static List<Transform> GetNearbyObjects(Transform obj, IEnumerable<Transform> objectsToGroup, float groupingRadius)
        {
            return objectsToGroup
                   .Where(otherObj => otherObj != obj && Vector3.Distance(obj.position, otherObj.position) <= groupingRadius)
                   .ToList();
        }

        public void CalculateGroupColliderSize(List<Transform> objectsToGroup, SphereCollider groupCollider)
        {
            if (objectsToGroup.Count <= 3)
            {
                groupCollider.center = new Vector3(0f, 1f, 0f);
                groupCollider.radius = 0.5f;
                return;
            }

            var minPosition = objectsToGroup[0].position;
            var maxPosition = objectsToGroup[0].position;

            foreach (var obj in objectsToGroup)
            {
                minPosition = Vector3.Min(minPosition, obj.position);
                maxPosition = Vector3.Max(maxPosition, obj.position);
            }

            var center = (minPosition + maxPosition) / 2f;
            var size = (maxPosition - minPosition) * 0.75f;

            groupCollider.center = new Vector3(0f, center.y, .5f);
            groupCollider.radius = size.x;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Services.ObjectGrouper
{
    public class ObjectGrouper : IObjectGrouper
    {
        public void GroupObjects(List<Transform> objectsToGroup, float groupingRadius)
        {
            foreach (Transform obj in objectsToGroup)
            {
                List<Transform> nearbyObjects = GetNearbyObjects(obj, objectsToGroup, groupingRadius);
                int numNearbyObjects = nearbyObjects.Count;
                float angleBetweenObjects = 360.0f / numNearbyObjects;
                for (int i = 0; i < numNearbyObjects; i++)
                {
                    float angle = i * angleBetweenObjects;
                    Vector3 newPosition = obj.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * groupingRadius;
                    nearbyObjects[i].position = newPosition;
                }
            }
        }
    
        private List<Transform> GetNearbyObjects(Transform obj, List<Transform> objectsToGroup, float groupingRadius)
        {
            List<Transform> nearbyObjects = new List<Transform>();
            foreach (Transform otherObj in objectsToGroup)
            {
                if (otherObj != obj && Vector3.Distance(obj.position, otherObj.position) <= groupingRadius)
                {
                    nearbyObjects.Add(otherObj);
                }
            }

            return nearbyObjects;
        }

        public void CalculateGroupColliderSize(List<Transform> objectsToGroup, SphereCollider groupCollider)
        {
            if (objectsToGroup.Count <= 3)
            {
                groupCollider.center = new Vector3(0f, 1f, 0f);
                groupCollider.radius = 0.5f;
                return;
            }

            Vector3 minPosition = objectsToGroup[0].position;
            Vector3 maxPosition = objectsToGroup[0].position;

            foreach (Transform obj in objectsToGroup)
            {
                minPosition = Vector3.Min(minPosition, obj.position);
                maxPosition = Vector3.Max(maxPosition, obj.position);
            }

            Vector3 center = (minPosition + maxPosition) / 2f;
            Vector3 size = (maxPosition - minPosition) * 0.75f;

            groupCollider.center = new Vector3(0f, center.y, .5f);
            groupCollider.radius = size.x;
        }
    }
}

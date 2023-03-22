using UnityEngine;

namespace Services.ObjectMover
{
    public class ObjectMover : IObjectMover
    {
        private float speed = 10f;
        private bool isMoveAvailable;

        public void UpdateObjectPosition(Transform t, Vector3 direction)
        {
            if(!isMoveAvailable) return;
        
            t.Translate(direction * speed * Time.deltaTime);
        }

        public void MoveAction(bool isAvailable)
        {
            isMoveAvailable = isAvailable;
        }

        public void MoveTowardsToObject(Transform t, Vector3 target)
        {
            t.position = Vector3.MoveTowards(t.position, target, speed * Time.deltaTime);
        }
    }
}

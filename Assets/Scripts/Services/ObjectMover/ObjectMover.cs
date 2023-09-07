using Logic;
using UnityEngine;

namespace Services.ObjectMover
{
    public class ObjectMover : IObjectMover
    {
        private bool  _isMoveAvailable;

        public float Speed { get; set; } = 10f;


        public void UpdateObjectPosition(Transform t, Vector3 direction)
        {
            if(!_isMoveAvailable) return;
        
            t.Translate(direction * (Speed * Time.deltaTime));
        }

        public void MoveAction(bool isAvailable)
        {
            _isMoveAvailable = isAvailable;
        }

        public void MoveTowardsToObject(Transform t, Vector3 target)
        {
            t.position = Vector3.MoveTowards(t.position, target, Speed * Time.deltaTime);
        }
    }
}

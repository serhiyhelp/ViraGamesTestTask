using UnityEngine;

namespace Services.ObjectMover
{
    public class ObjectMover : IObjectMover
    {
        private float _speed = 10f;
        private bool  _isMoveAvailable;

        public void UpdateObjectPosition(Transform t, Vector3 direction)
        {
            if(!_isMoveAvailable) return;
        
            t.Translate(direction * (_speed * Time.deltaTime));
        }

        public void MoveAction(bool isAvailable)
        {
            _isMoveAvailable = isAvailable;
        }

        public void MoveTowardsToObject(Transform t, Vector3 target)
        {
            t.position = Vector3.MoveTowards(t.position, target, _speed * Time.deltaTime);
        }
    }
}

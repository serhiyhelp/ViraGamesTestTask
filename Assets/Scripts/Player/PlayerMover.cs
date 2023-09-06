using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerMover
    {
        private readonly IInputService _inputService;
        private readonly float         _roadHalfWidth;
        private readonly float         _speed;
        
        private bool _isDragging = false;
        private Vector2 _startPos;

        public PlayerMover(IInputService inputService, float roadHalfWidth, float speed)
        {
            _inputService  = inputService;
            _roadHalfWidth = roadHalfWidth;
            _speed         = speed;
        }

        public void UpdatePosMobile(Transform t)
        {
            if (_inputService.OnClicked)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startPos = touch.position;
                        _isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isDragging)
                        {
                            var deltaX = touch.position.x - _startPos.x;
                            var position = t.position;
                            var newX = position.x + deltaX * _speed * Time.deltaTime;
                            
                            newX = Mathf.Clamp(newX, -_roadHalfWidth, _roadHalfWidth);
                            
                            position = new Vector3(newX, position.y, position.z);
                            t.position = position;

                            _startPos = touch.position;
                        }
                        break;

                    case TouchPhase.Ended:
                        _isDragging = false;
                        break;
                }
            }
        }

        public void UpdatePosStandalone(Transform t)
        {
            if (_inputService.OnClicked)
            {
                _startPos = Input.mousePosition;
                _isDragging = true;
            }
            if (Input.GetMouseButton(0))
            {
                if (_isDragging)
                {
                    var deltaX = Input.mousePosition.x - _startPos.x;
                    var position = t.position;
                    var newX = position.x + deltaX * _speed * Time.deltaTime;
                    newX = Mathf.Clamp(newX, -_roadHalfWidth, _roadHalfWidth);
                    position = new Vector3(newX, position.y, position.z);
                    t.position = position;
                    _startPos = Input.mousePosition;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }
        }
    }
}
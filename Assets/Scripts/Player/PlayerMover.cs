using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerMover
    {
        private readonly IInputService _inputService;

        private const float Speed = 1.2f;
        private const float MinX = -2.75f;
        private const float MaxX = 2.75f;
        private bool _isDragging = false;
        private Vector2 _startPos;

        public PlayerMover(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void UpdatePosMobile(Transform t)
        {
            if (_inputService.OnClicked)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startPos = touch.position;
                        _isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isDragging)
                        {
                            float deltaX = touch.position.x - _startPos.x;
                            var position = t.position;
                            float newX = position.x + deltaX * Speed * Time.deltaTime;
                            
                            newX = Mathf.Clamp(newX, MinX, MaxX);
                            
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
                    float deltaX = Input.mousePosition.x - _startPos.x;
                    var position = t.position;
                    float newX = position.x + deltaX * Speed * Time.deltaTime;
                    newX = Mathf.Clamp(newX, MinX, MaxX);
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
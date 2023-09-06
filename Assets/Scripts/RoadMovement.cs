using Infrastructure.Services;
using Services.ObjectMover;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    
    private IObjectMover _objectMover;

    private void Awake()
    {
        _objectMover = AllServices.Container.Single<IObjectMover>();
    }

    private void Update()
    {
        _objectMover.UpdateObjectPosition(transform, Vector3.back);
        
        if (transform.position.z < _endPoint.position.z)
        {
            transform.position = _startPoint.position;
        }
    }
}
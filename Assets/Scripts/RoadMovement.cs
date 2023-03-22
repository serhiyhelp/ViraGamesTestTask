using Infrastructure.Services;
using Services.ObjectMover;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float threshold = 0.1f;
    private IObjectMover _objectMover;

    private void Awake()
    {
        _objectMover = AllServices.Container.Single<IObjectMover>();
    }

    private void Update()
    {
        _objectMover.UpdateObjectPosition(transform, Vector3.back);
        
        if (Vector3.Distance(transform.position, endPoint.position) < threshold)
        {
            transform.position = startPoint.position;
        }
    }
}
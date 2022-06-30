using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ReflectingLaserController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    
    [SerializeField]
    private int _maxReflectionCount = 10;
    [SerializeField]
    private float _maxRayDistance = 20f;
    
    private bool _hasMoved = true;

    private Vector3 _lastPosition = Vector3.zero;
    private Quaternion _lastRotation = Quaternion.identity;
    
    // Update is called once per frame
    void Update()
    {
        if (_lastPosition != transform.position || _lastRotation != transform.rotation)
        {
            _hasMoved = true;
            _lastPosition = transform.position;
            _lastRotation = transform.rotation;
        }
        
        if (_hasMoved)
        {
            int positionCount = _maxReflectionCount + 2;
            float distanceLeft = _maxRayDistance;
            _lineRenderer.positionCount = positionCount; // Add 2 to count the beginning and end

            _lineRenderer.SetPosition(0, _lineRenderer.transform.position);
            Vector3 rayDirection = _lineRenderer.transform.forward;
            for (int i = 1; i < positionCount; i++)
            {
                Vector3 startPos = _lineRenderer.GetPosition(i - 1);
                bool obstacleHit = Physics.Raycast(startPos, rayDirection, out RaycastHit hit, distanceLeft);
                if (obstacleHit)
                {
                    distanceLeft -= hit.distance;
                    rayDirection = Vector3.Reflect(rayDirection, hit.normal); 
                    _lineRenderer.SetPosition(i, hit.point);
                }
                else
                {
                    _lineRenderer.SetPosition(i, startPos + rayDirection * distanceLeft);

                    _lineRenderer.positionCount = i + 1;
                    distanceLeft = 0;
                    break;
                }
            }
            _hasMoved = false;
        }
    }
}

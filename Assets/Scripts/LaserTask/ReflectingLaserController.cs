using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ReflectingLaserController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    /// <summary>
    /// The max amount of reflections the laser can make, before disappearing.
    /// </summary>
    [Space]
    [SerializeField]
    private int _maxReflectionCount = 10;

    /// <summary>
    /// The max distance the laser can travel, before disappearing.
    /// </summary>
    [SerializeField]
    private float _maxRayDistance = 20f;


    /// <summary>
    /// Indicates, if the laser should be on by default
    /// </summary>
    [SerializeField]
    private bool _isOnByDefault = false;


    /// <summary>
    /// Indicates, if the laser should respond to mouse input.
    /// </summary>
    [SerializeField]
    private bool _controlledByMouse = true;

    /// <summary>
    /// Indicates if the laser has moved since last frame.
    /// </summary>
    private bool _hasMoved = true;

    #region  last position and rotation of the laser gun
    private Vector3 _lastPosition = Vector3.zero;
    private Quaternion _lastRotation = Quaternion.identity;

    #endregion

    private bool IsLaserEnabled
    {
        get { return _lineRenderer.enabled; }
        set { _lineRenderer.enabled = value; }
    }


    // Update is called once per frame
    void Update()
    {
        HandleLaserStatus();

        CheckIfLaserHasMoved();

        if (IsLaserEnabled && _hasMoved)
        {
            FireLaser();
        }

    }

    /// <summary>
    /// Check if the laser gun has moved since last update, to avoid unnecesary calculations.
    /// </summary>
    private void CheckIfLaserHasMoved()
    {
        if (_lastPosition != transform.position || _lastRotation != transform.rotation)
        {
            _hasMoved = true;
            _lastPosition = transform.position;
            _lastRotation = transform.rotation;
        }
    }

    /// <summary>
    /// Fire a beam from the line renderer, and calculate its reflections.
    /// </summary>
    private void FireLaser()
    {
        if (_hasMoved)
        {
            int positionCount = _maxReflectionCount + 2; // +2 for the start and end point
            float distanceLeft = _maxRayDistance;
            _lineRenderer.positionCount = positionCount;

            _lineRenderer.SetPosition(0, _lineRenderer.transform.position);
            Vector3 rayDirection = _lineRenderer.transform.forward;


            for (int i = 1; i < positionCount; i++)
            {
                Vector3 startPos = _lineRenderer.GetPosition(i - 1);
                bool obstacleHit = Physics.Raycast(startPos, rayDirection, out RaycastHit hit, distanceLeft);
                if (i + 1 < positionCount) // no need to calculate the next reflection, if the ray has run out of them
                {

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
            }
            _hasMoved = false;
        }
    }


    /// <summary>
    /// Controlls the firing state of the laser.
    /// </summary>
    private void HandleLaserStatus()
    {

        if (_controlledByMouse)
        {
            // If _isOnByDefault is false, fire the laser only on pressed mouse button.
            // If _isOnByDefault is true, fire the laser only on released mouse button.
            bool targetValueOnPressed = true; // the value to which the laser is set to, when the button is pressed down. Swapped when _isOnByDefault is true. 
            if (_isOnByDefault)
            {
                targetValueOnPressed = false;
            }
            if (Input.GetMouseButton(0))
            {
                ToggleLaser(targetValueOnPressed);
            }
            else
            {
                ToggleLaser(!targetValueOnPressed);
            }
        }
        else
        {
            ToggleLaser(_isOnByDefault);
        }

    }

    /// <summary>
    /// Turn the laser on or off. This name is not the best description, but it feels more natural.
    /// </summary>
    private void ToggleLaser(bool targetState)
    {
        if (IsLaserEnabled != targetState)
        {
            IsLaserEnabled = targetState;
        }
    }


}

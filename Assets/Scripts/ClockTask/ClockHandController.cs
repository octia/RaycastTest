using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandController : MonoBehaviour
{

    // offset the angle of the hand, to make the starting position be at the top of the clock
    [SerializeField]
    private float _angleOffset;

    /// <summary>
    /// The angle of the hand, in degrees, starting from the top of the clock, going clockwise.
    /// </summary>
    public void SetAngle(float angle)
    {
        float angleToSet = _angleOffset; // this takes into account the base offset of the hand
        angleToSet -= angle; // and this makes it go clockwise
        
        transform.localRotation = Quaternion.Euler(angleToSet, 0, 0); 
    }
}

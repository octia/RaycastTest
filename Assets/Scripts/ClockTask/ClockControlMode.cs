using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The different types in which the clock can operate.
/// </summary>
public enum ClockControlMode 
{
    /// <summary>
    /// The clock will show the current time.
    /// </summary>
    CurrentTime,

    /// <summary>
    /// The clock will show a custom time.
    /// </summary>
    CustomTime,

    /// <summary>
    /// The clock will show move from the currently shown time, according to the acceleration. Can result in moving backwards.
    /// </summary>
    Acceleration,
}

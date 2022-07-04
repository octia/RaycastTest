using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    
    [SerializeField]
    ClockHandController _hourHand;
    [SerializeField]
    ClockHandController _minuteHand;
    [SerializeField]
    ClockHandController _secondHand;

    [Space]
    [SerializeField]
    ClockControlMode _clockControllMode = ClockControlMode.CurrentTime;

    [Space]
    [InspectorLabel("Clock Time")]
    [SerializeField, Range(0, 11)]
    private int _hour;
    [SerializeField, Range(0, 59)]
    private int _minute;
    [SerializeField, Range(0, 59)]
    private int _second;
    [SerializeField, Range(0, 999)]
    private int _milisecond;

    private DateTime _shownTime;
    

    [SerializeField]
    private float _clockAcceleration = 1f;


    // Update is called once per frame
    void Update()
    {   
        GetNextTime();
        SetTime(_shownTime);
    }

    // Set _shownTime to a new value, according to the current clock control mode
    private void GetNextTime()
    {
        switch (_clockControllMode)
        {
            case ClockControlMode.CurrentTime:
                _shownTime = DateTime.Now;
                UpdateTimeValues();
                break;
            case ClockControlMode.CustomTime:
                _shownTime = new DateTime(_shownTime.Year, _shownTime.Month, _shownTime.Day, _hour, _minute, _second, _milisecond);
                break;
            case ClockControlMode.Acceleration:
                _shownTime += new TimeSpan(0,0,0,0, (int)(Time.deltaTime * 1000 * _clockAcceleration));
                UpdateTimeValues();
                break;
        }
    }


    /// <summary>
    /// Update the time values of the inspector variables during non-custom control modes, for a cool effect of changing values in the inspector
    /// </summary>
    private void UpdateTimeValues()
    {
        _hour = _shownTime.Hour;
        _minute = _shownTime.Minute;
        _second = _shownTime.Second;
        _milisecond = _shownTime.Millisecond;
    }


    /// <summary>
    /// Set the clock hands to a given time
    /// </summary>
    private void SetTime(DateTime time)
    {
        float hourAngle = time.Hour * 30f + time.Minute * 0.5f;
        float minuteAngle = time.Minute * 6f + time.Second * 0.1f;
        float secondAngle = time.Second * 6f + time.Millisecond * 0.006f;

        _hourHand.SetAngle(hourAngle);
        _minuteHand.SetAngle(minuteAngle);
        _secondHand.SetAngle(secondAngle);
    }


}

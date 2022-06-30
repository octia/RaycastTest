using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectingLaserController : MonoBehaviour
{
    [SerializeField]
    private readonly LineRenderer _lineRenderer;
    
    [SerializeField]
    private readonly int _maxReflectionCount = 10;
    [SerializeField]
    private readonly float _maxRayDistance = 20f;
    
    private bool _hasMoved = true;

    // Start is called before the first frame update
    void Start()
    {
        
        _hasMoved = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

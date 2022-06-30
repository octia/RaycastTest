using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _rotatingObj;

    [SerializeField, Range(0, 1f)]
    private float _mouseSensitivity = 0.3f;

    [SerializeField, Range(0, 30f)]
    private float _movementSpeed = 4f;

    private Vector2 _lastCursorPos = -Vector2.one;

    Vector2 _cameraRotation = Vector2.zero;

    // Update is called once per frame
    void Update()
    {

        HandleMouseInput();
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }

    private void HandleMouseInput()
    {
        if (_lastCursorPos == -Vector2.one)
        {
            _lastCursorPos = Input.mousePosition;
            
        }
        Vector2 currCursorPos = Input.mousePosition;
        if (_lastCursorPos != currCursorPos)
        {
            Vector2 delta = Input.GetAxis("Mouse X") * Vector2.right + Input.GetAxis("Mouse Y") * Vector2.up;
            delta *= Time.deltaTime * _mouseSensitivity;
            delta *= 360f;
            _cameraRotation += delta;

            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -90f, 90f);

            // Rotate the camera and the player on both x and y axis separately, allowing for simple 2D movement implementation
            _camera.transform.eulerAngles = new Vector3(-_cameraRotation.y, _cameraRotation.x, 0);
            
            
            _lastCursorPos = currCursorPos;
        }

    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += (_rotatingObj.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += (-_rotatingObj.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += (-_rotatingObj.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += (_rotatingObj.right);
        }
        movement.y = 0;
        movement = movement.normalized * _movementSpeed * Time.deltaTime;
        transform.Translate(movement);

    }

}

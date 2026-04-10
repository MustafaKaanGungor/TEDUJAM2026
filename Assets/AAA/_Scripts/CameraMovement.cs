using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
     private Camera _camera;
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private InputActionReference _actionReferance;
    private bool _isCameraUp = true;
    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        _camera = Camera.main;
        _inputActions.Enable();
        _inputActions.FindAction("CameraUp").performed += OnMoveUp;
        _inputActions.FindAction("CameraDown").performed += OnMoveDown;
        _actionReferance.action.performed += OnMove;
    }


    private void OnDisable()
    {
        _inputActions.FindAction("CameraUp").performed -= OnMoveUp;
        _inputActions.FindAction("CameraDown").performed -= OnMoveDown;
        _inputActions.Disable();
        _actionReferance.action.performed -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();

        if (value.y > 0 || !_isCameraUp)
        {
            OnMoveUp(context);
        }
        else if (value.y < 0||_isCameraUp)
        {
            OnMoveDown(context);
        }
    }

    private void OnMoveDown(InputAction.CallbackContext context)
    {
        Debug.Log("CameraDown");
        if (_isCameraUp)
        {
            _camera.transform.DORotate(new Vector3(90, 0, 0), 1f);
            _isCameraUp = false;
        }

    }

    private void OnMoveUp(InputAction.CallbackContext context)
    {
        Debug.Log("CameraUp");
        if (!_isCameraUp)
        {
            _camera.transform.DORotate(new Vector3(0, 0, 0), 1f);
            _isCameraUp = true;
        }
    }
}

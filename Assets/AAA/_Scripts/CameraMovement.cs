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
    [SerializeField] private Vector3 defaultRotation = new(0, 0, 0);
    [SerializeField] private Vector3 mapRotation = new(90, 0, 0);

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

        if (value.y > 0.5 || !_isCameraUp)
        {
            OnMoveUp(context);
        }
        else if (value.y < -0.5 ||_isCameraUp)
        {
            OnMoveDown(context);
        }
    }

    private void OnMoveDown(InputAction.CallbackContext context)
    {
        if (_isCameraUp)
        {
            _camera.transform.DORotate(mapRotation, 1f);
            _isCameraUp = false;
        }

    }

    private void OnMoveUp(InputAction.CallbackContext context)
    {
        if (!_isCameraUp)
        {
            _camera.transform.DORotate(defaultRotation, 1f);
            _isCameraUp = true;
        }
    }
}

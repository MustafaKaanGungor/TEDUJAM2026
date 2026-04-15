using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    private InputSystem_Actions _inputactions;



    private void Awake()
    {
        _inputactions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        _creditsButton.onClick.AddListener(OnCreditsClicked);
        _quitButton.onClick.AddListener(OnQuitClicked);
        _inputactions.UI.Cancel.performed += OnCancelPerformed;
        _inputactions.UI.Enable();
    }
    private void OnDisable()
    {
        _inputactions.UI.Cancel.performed -= OnCancelPerformed;
        _inputactions.UI.Disable();
        _creditsButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }
    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        if(_creditsMenu != null)
        {
            _creditsMenu.SetActive(false);
        }
    }
    private void OnCreditsClicked()
    {
        _creditsMenu?.SetActive(true);
    }
    private void OnQuitClicked()
    {
        Application.Quit();
    }
}

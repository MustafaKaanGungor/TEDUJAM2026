using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _tutorialSentenceText;
    [TextArea(3, 10)]
    [SerializeField] private string[] _sentences;
    private int _currentIndex = 0;
    [SerializeField] private InputActionReference _actionReference;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private GameObject _turorialPanel;
    private TMP_Text _continueButtonText;

    private void OnEnable()
    {
        _actionReference.action.performed += OnNextSentenceButtonPerformed;
        _continueButton.onClick.AddListener(OnNextButtonClicked);
        _previousButton.onClick.AddListener(OnPreviousButtonClicked);
        GameEvents.ShowTutorial_Game += OnShowTutorial;
        _previousButton.interactable = false;
    }
    private void Start()
    {
        _continueButtonText = _continueButton.GetComponentInChildren<TMP_Text>();
        ShowSentence(_currentIndex);
    }


    private void OnDisable()
    {
        _actionReference.action.performed -= OnNextSentenceButtonPerformed;
        _continueButton.onClick.RemoveListener(OnNextButtonClicked);
        GameEvents.ShowTutorial_Game -= OnShowTutorial;
        _previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
    }
    public void OnNextSentenceButtonPerformed(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (_currentIndex < _sentences.Length - 1)
        {
            _currentIndex++;
            ShowSentence(_currentIndex);
            _previousButton.interactable = true;
            if (_currentIndex == _sentences.Length -1)
            {
                Debug.Log("text changed");
                _continueButtonText.text = "I get it";
            }
        }
        else
        {
            ExitTutorial();
        }
    }
    public void OnNextButtonClicked()
    {
        if (_currentIndex < _sentences.Length - 1)
        {
            _currentIndex++;
            ShowSentence(_currentIndex);
            _previousButton.interactable = true;
            if (_currentIndex == _sentences.Length - 1)
            {
                _continueButtonText.text = "I get it";
            }
        }
        else
        {
            ExitTutorial();
        }
    }
    public void OnPreviousButtonClicked()
    {
        _currentIndex--;
        ShowSentence(_currentIndex);
        if (_currentIndex == 0)
        {
            _previousButton.interactable = false;
        }

    }
    public void ShowSentence(int index)
    {
        _tutorialSentenceText.text = _sentences[_currentIndex];
    }
    private void OnShowTutorial()
    {
        _turorialPanel.SetActive(true);
    }
    private void ExitTutorial()
    {
        GameEvents.TutorialFinished_TutorialUI?.Invoke();
        _turorialPanel.SetActive(false);
    }

}

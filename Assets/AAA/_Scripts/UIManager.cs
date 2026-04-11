using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _dayText;
    private void OnEnable()
    {
        GameEvents.DayChanged += UpdateDayUI;
    }

    private void OnDisable()
    {
        GameEvents.DayChanged -= UpdateDayUI;
    }

    private void UpdateDayUI(int day)
    {
        // Update the UI with the new day value
        _dayText.text = $"Day: {day}";
        GameEvents.PlaySound?.Invoke("Morning");
    }
}
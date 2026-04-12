using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private TMP_Text deadPeopleStatText;
    [SerializeField] private TMP_Text successfulPeopleStatText;
    [SerializeField] private List<Image> imageList = new List<Image>();
    private Image[,] acutalMapImages = new Image[5,5];
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite island1Sprite;
    [SerializeField] private Sprite island2Sprite;
    [SerializeField] private Sprite island3Sprite;
    [SerializeField] private Sprite island4Sprite;
    [SerializeField] private Sprite island5Sprite;
    [SerializeField] private Sprite island6Sprite;
    [SerializeField] private Sprite danger1Sprite;
    [SerializeField] private Sprite danger2Sprite;
    [SerializeField] private Sprite danger3Sprite;
    [SerializeField] private Sprite landmark1Sprite;
    [SerializeField] private Sprite landmark2Sprite;
    [SerializeField] private Sprite landmark3Sprite;
    [SerializeField] private Sprite landmark4Sprite;
    [SerializeField] private Sprite baseSprite;

    private void OnEnable()
    {
        GameEvents.DayChanged += UpdateDayUI;
        GameEvents.GameEnd += OnGameEnd;
    }

    private void OnDisable()
    {
        GameEvents.DayChanged -= UpdateDayUI;
        GameEvents.GameEnd -= OnGameEnd;
    }

    private void UpdateDayUI(int day)
    {
        // Update the UI with the new day value
        _dayText.text = $"Day: {day}";
        //GameEvents.PlaySound?.Invoke("Morning");
    }

    private void OnGameEnd(int arg1, int arg2, MapNode[,] map)
    {


        gameEndScreen.SetActive(true);

        deadPeopleStatText.text = arg1 + " people sacrified their lives for your map";
        successfulPeopleStatText.text = arg2 + " lives saved thanks to your map";
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                acutalMapImages[i,j] = imageList[i * 5 + j];
            }
        }

        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                acutalMapImages[i,j].sprite = GetMapImage(map[i,j].type);
            }
        }
    }

    private Sprite GetMapImage(IslandType type)
    {
        switch (type)
        {
            case IslandType.EMPTY:
                return emptySprite;
            case IslandType.ISLAND1:
                return island1Sprite;
            case IslandType.ISLAND2:
                return island2Sprite;
            case IslandType.ISLAND3:
                return island3Sprite;
            case IslandType.ISLAND4:
                return island4Sprite;
            case IslandType.ISLAND5:
                return island5Sprite;
            case IslandType.ISLAND6:
                return island6Sprite;
            case IslandType.DANGER1:
                return danger1Sprite;
            case IslandType.DANGER2:
                return danger2Sprite;
            case IslandType.DANGER3:
                return danger3Sprite;
            case IslandType.LANDMARK1:
                return landmark1Sprite;
            case IslandType.LANDMARK2:
                return landmark2Sprite;
            case IslandType.LANDMARK3:
                return landmark3Sprite;
            case IslandType.LANDMARK4:
                return landmark4Sprite;
            case IslandType.BASE:
                return baseSprite;
            default:
            return null;
        }
    }
}
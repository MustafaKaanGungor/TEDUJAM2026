using System;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    private int deadPeopleAmount = 0;
    private int successfulPeopleAmount = 0;

    void Start()
    {
        GameEvents.NpcDied += OnNpcDied;
        GameEvents.NpcSuccessful += OnNpcSuccessful;
    }

    private void OnNpcDied()
    {
        deadPeopleAmount++;
    }

    private void OnNpcSuccessful()
    {
        successfulPeopleAmount++;
    }

    public (int, int) GetStats()
    {
        return (deadPeopleAmount, successfulPeopleAmount);
    }
}

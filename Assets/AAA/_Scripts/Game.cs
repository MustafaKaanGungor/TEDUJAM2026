using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private List<NpcData> _allNpcs = new();
    private List<Npc> _currentNpcs = new();
    private int _currentDay = 0;
    [SerializeField] private GameObject _npcPrefab;
    [SerializeField] private Transform _spawnTransform;
    private MapGenerator map;
    [SerializeField] private NpcData data;

    private void Awake() {
        map = GetComponent<MapGenerator>();
    }

    private void Start()
    {
        DayStarted();
    }
    private async void StartDayCycle()
    {
        await DayCycleAsync();
    }
    private async Task DayCycleAsync()
    {
        while (true)
        {
            // Simulate day cycle logic here
            Debug.Log("Day cycle is running...");
            await Task.Delay(1000); // Simulate time passing
        }
    }


    private void AddNpcToList()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_currentNpcs[i] != null)
            {
                continue;
            }
            else
            {
                var npc = new GameObject($"Npc_{i}").AddComponent<Npc>();
                npc.Initialize(_allNpcs[i]);
                _currentNpcs.Add(npc);

            }
        }
    }
    private void DayStarted()
    {
        _currentDay++;
        GameEvents.DayChanged?.Invoke(_currentDay);
        GameEvents.PlaySound?.Invoke("Morning");
        GameObject newNpc = Instantiate(_npcPrefab, _spawnTransform.position, Quaternion.identity);
        newNpc.GetComponent<Npc>().Initialize(data);
    }
    private void DayFinished()
    {
        GameEvents.PlaySound?.Invoke("Evening");
    }
}

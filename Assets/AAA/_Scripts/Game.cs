using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    [SerializeField] private List<NpcData> _allNpcs = new();
    private List<Npc> _currentNpcs = new();
    private int _currentDay = 0;
    [SerializeField] private GameObject _npcPrefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private InputActionReference _action;
    private bool _canPerform = false;

    private void OnEnable()
    {
        _action.action.performed += ChangeInputAuthorityToNpc;
        GameEvents.ChangeInputAuthorityToPlayer += OnChangeInputAuthorityToPlayer;
    }



    private void OnDisable()
    {
        _action.action.performed -= ChangeInputAuthorityToNpc;
        GameEvents.ChangeInputAuthorityToPlayer -= OnChangeInputAuthorityToPlayer;
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
                var newNpc = Instantiate(_npcPrefab, _spawnTransform.position, Quaternion.identity);
                newNpc.SetActive(false);
                var npc = newNpc.GetComponent<Npc>();
                //npc.Initialize(_allNpcs[i]);
                _currentNpcs.Add(npc);

            }
        }
    }
    private void DayStarted()
    {
        _currentDay++;
        AddNpcToList();
        GameEvents.DayChanged?.Invoke(_currentDay);
        GameEvents.PlaySound?.Invoke("Morning");
        //lsiteden 1. npc yi getir
    }
    private void DayFinished()
    {
        GameEvents.PlaySound?.Invoke("Evening");
    }
    private void ChangeInputAuthorityToNpc(InputAction.CallbackContext context)
    {
        if (_canPerform)
        {
            _canPerform = false;
            GameEvents.ChangeInputAuthorityToNpc?.Invoke();
        }

    }
    private void OnChangeInputAuthorityToPlayer()
    {
        _canPerform = true;
    }
    private void GetCurrentNpcFromList()
    {

    }
}

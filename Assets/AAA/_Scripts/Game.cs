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
    private List<GameObject> _currentNpcs = new();
    private int _currentDay = 0;
    [SerializeField] private GameObject _npcPrefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private InputActionReference _action;
    private bool _canPerform = false;
    private int _currentNpcIndex = 0;
    private int _nextNpcIndex = 0;
    private const int MAX_NPC_COUNT = 3;
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
        Debug.Log($"Gün {_currentDay} baþladý. NPC sayýsý: {_currentNpcs.Count}");

        // Snapshot al — döngü içinde liste deðiþecek
        var todayNpcs = new List<GameObject>(_currentNpcs);

        foreach (var npcObject in todayNpcs)
        {
            npcObject.SetActive(true);
            Npc npc = npcObject.GetComponent<Npc>();

            // NPC turunu bitirene kadar bekle
            TaskCompletionSource<bool> tcs = new();
            Action onFinished = null;
            onFinished = () =>
            {
                npc.OnNpcFinished -= onFinished;
                tcs.TrySetResult(true);
            };
            npc.OnNpcFinished += onFinished;
            await tcs.Task;

            npcObject.SetActive(false);

            if (npc.IsDead)
            {
                Debug.Log($"{npc.name} öldü. Yarýn yerine yenisi gelecek.");
                _currentNpcs.Remove(npcObject);
                Destroy(npcObject);
            }
            else
            {
                Debug.Log($"{npc.name} sað ayrýldý. Yarýn geri gelecek.");
            }

            await Task.Delay(2000);
        }

        Debug.Log("Tüm NPC'ler bitti. Gün sonlanýyor...");
        DayFinished();
        DayStarted();
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
                npc.Initialize(_allNpcs[i]);
                _allNpcs.RemoveAt(i);
                _currentNpcs.Add(newNpc);
            }
        }
    }
    private void DayStarted()
    {
        _currentDay++;
        FillNpcSlots();                             // Boþ slotlarý doldur
        GameEvents.DayChanged?.Invoke(_currentDay);
        GameEvents.PlaySound?.Invoke("Morning");
        _ = DayCycleAsync();
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
    private GameObject GetCurrentNpcFromList()
    {
        _currentNpcIndex++;
        return _currentNpcs[_currentNpcIndex - 1];
    }
    private void FillNpcSlots()
    {
        int slotsNeeded = MAX_NPC_COUNT - _currentNpcs.Count;

        for (int i = 0; i < slotsNeeded; i++)
        {
            if (_allNpcs.Count == 0)
            {
                Debug.LogWarning("Eklenecek yeni NPC kalmadý!");
                break;
            }

            NpcData data = _allNpcs[0];
            _allNpcs.RemoveAt(0);

            GameObject newNpcObj = Instantiate(_npcPrefab, _spawnTransform.position, Quaternion.identity);
            newNpcObj.SetActive(false);

            Npc npc = newNpcObj.GetComponent<Npc>();
            npc.Initialize(data);

            _currentNpcs.Add(newNpcObj);
            Debug.Log($"Yeni NPC eklendi: {data.name}. Aktif NPC sayýsý: {_currentNpcs.Count}");
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private List<NpcData> _allNpcs => _allNPCS;
    [SerializeField] private List<NpcData> _allNPCS = new();
    private List<GameObject> _currentNpcs = new();
    private int _currentDay = 0;
    [SerializeField] private GameObject _npcPrefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private InputActionReference _action;
    private bool _canPerform = false;
    private int _currentNpcIndex = 0;
    private const int MAX_NPC_COUNT = 3;
    private MapGenerator map;
    private StatTracker _statTracker;
    private static System.Random rng = new System.Random();
    //private MapNode[,] _map;
    private void OnEnable()
    {

        _action.action.performed += ChangeInputAuthorityToNpc;
        GameEvents.ChangeInputAuthorityToPlayer += OnChangeInputAuthorityToPlayer;
        GameEvents.MapGenerated += OnMapGenerated;
        GameEvents.TutorialFinished_TutorialUI += OnTutorialFinished;
    }



    private void OnDisable()
    {
        _action.action.performed -= ChangeInputAuthorityToNpc;
        GameEvents.ChangeInputAuthorityToPlayer -= OnChangeInputAuthorityToPlayer;
        GameEvents.MapGenerated -= OnMapGenerated;
        GameEvents.TutorialFinished_TutorialUI -= OnTutorialFinished;


    }



    private void Awake() {
        map = GetComponent<MapGenerator>();
        _statTracker = GetComponent<StatTracker>();
        var shuffledNpcs = _allNpcs.OrderBy(a => rng.Next()).ToList();
        _allNPCS = shuffledNpcs;
    }
    private void OnMapGenerated(MapNode[,] obj)
    {
        
    }


    private void Start()
    {
        GameEvents.PlaySound("Ost");
        GameEvents.ShowTutorial_Game?.Invoke();
    }
    private void StartDayCycle()
    {
        StartCoroutine(DayCycleCoroutine());
    }

    private IEnumerator DayCycleCoroutine()
    {
        var todayNpcs = new List<GameObject>(_currentNpcs);

        foreach (var npcObject in todayNpcs)
        {
            npcObject.SetActive(true);
            Npc npc = npcObject.GetComponent<Npc>();

            // Önce event'e subscribe ol
            bool npcFinished = false;
            Action onFinished = null;
            onFinished = () =>
            {
                npc.OnNpcFinished -= onFinished;
                npcFinished = true;
            };
            npc.OnNpcFinished += onFinished;

            // Sonra NPC'yi başlat — artık race condition yok
            npc.StartNpc();

            yield return new WaitUntil(() => npcFinished);

            if (npc.IsDead)
            {
                _currentNpcs.Remove(npcObject);
                Destroy(npcObject);
            }
            else
            {
                GameEvents.NpcSuccessful?.Invoke();
                npcObject.transform.position = _spawnTransform.position;

                if (npc.NpcData.LastVisitedIsland == npc.NpcData.DesiredIsland)
                    npc.NpcData.DesiredIsland = GetDesiredIsland(npc.NpcData.DesiredIsland);
            }

            npcObject.SetActive(false);
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Tüm NPC'ler bitti. Gün sonlanıyor...");
        DayFinished();
        DayStarted();
    }
    private void DayStarted()
    {
        GameEvents.PlaySound?.Invoke("Cock");
        _currentDay++;
        FillNpcSlots();                             
        GameEvents.DayChanged?.Invoke(_currentDay);
        GameEvents.ChangeInputAuthorityToNpc?.Invoke();

        if (_currentDay == 7) 
        {
            var (arg1,arg2) = _statTracker.GetStats();
            GameEvents.GameEnd?.Invoke(arg1,arg2,map.mapArray);
            return;
        }
        StartDayCycle();
    }
    private void DayFinished()
    {
        //GameEvents.PlaySound?.Invoke("");
    }
    private void ChangeInputAuthorityToNpc(InputAction.CallbackContext context)
    {
        if (_canPerform)
        {
            //ui da kitlenmeli
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
                Debug.LogWarning("Eklenecek yeni NPC kalmad�!");
                break;
            }
            NpcData data = _allNpcs[0];
            _allNpcs.RemoveAt(0);
            data.Dialogues.direction1 = (Direction) UnityEngine.Random.Range(0, 8);
            while (true)
            {
                Direction secondDirection = (Direction)UnityEngine.Random.Range(0, 8);
                if (secondDirection != data.Dialogues.direction1)
                {
                    data.Dialogues.direction2 = secondDirection;
                    break;
                }
            }
            data.DesiredIsland = GetDesiredIsland();
            data.Dialogues.islandOnDirection1 = map.mapArray[2 + (int)GetDirectionMovement(data.Dialogues.direction1).x,2 + (int)GetDirectionMovement(data.Dialogues.direction1).y].type;
            data.Dialogues.islandOnDirection2 = map.mapArray[2 + (int)GetDirectionMovement(data.Dialogues.direction1).x + (int)GetDirectionMovement(data.Dialogues.direction2).x,2 + (int)GetDirectionMovement(data.Dialogues.direction1).y + (int)GetDirectionMovement(data.Dialogues.direction2).y].type;
            GameObject newNpcObj = Instantiate(_npcPrefab, _spawnTransform.position, Quaternion.identity);
            newNpcObj.SetActive(false);
            Npc npc = newNpcObj.GetComponent<Npc>();
            npc.Initialize(data,map);
            _currentNpcs.Add(newNpcObj);
        }
    }

    private Vector2 GetDirectionMovement(Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                return new Vector2(-1, 0);
            case Direction.NORTHEAST:
                return new Vector2(-1, 1);
            case Direction.EAST:
                return new Vector2(0, 1);
            case Direction.SOUTHEAST:
                return new Vector2(1, 1);
            case Direction.SOUTH:
                return new Vector2(1, 0);
            case Direction.SOUTHWEST:
                return new Vector2(1, -1);
            case Direction.WEST:
                return new Vector2(0, -1);
            case Direction.NORTHWEST:
                return new Vector2(-1, -1);
            default:
                return new Vector2(0, 0);
        }
    }
    private IslandType GetDesiredIsland(IslandType islandToAvoid = default)
    {
        List<IslandType> availableIslands = new List<IslandType>();

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                IslandType currentType = map.mapArray[x, y].type;
                if (currentType >= IslandType.ISLAND1 && currentType <= IslandType.ISLAND6)
                {
                    if (currentType != islandToAvoid)
                    {
                        availableIslands.Add(currentType);
                    }
                }
            }
        }
        if (availableIslands.Count == 0)
        {
            return default; 
        }
        return availableIslands[UnityEngine.Random.Range(0, availableIslands.Count)];
    }
    private void OnTutorialFinished()
    {
        DayStarted();
    }
}

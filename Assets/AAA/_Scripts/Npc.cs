using DG.Tweening;
using System;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [HideInInspector] public NpcData NpcData;
    private SpriteRenderer _spriteRenderer;
    private Stage _currentStage;
    private bool _isPerforming = true;
    private bool _canMove = false;
    [Header("Jump Settings")]
    //[SerializeField] private Vector3 _spawnTransform;
    [SerializeField] private Vector3 _centerTarget;
    [SerializeField] private float _jumpPower = 2f;
    [SerializeField] private int _numJumps = 1;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private Vector3 _endTarget;
    public event Action OnNpcFinished;
    private MapGenerator _map;
    public bool IsDead { get; private set; } = false;
    public void Initialize(NpcData npcData, MapGenerator mapNodes)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        NpcData = npcData;
        _spriteRenderer.sprite = NpcData.NpcSprite;
        _map = mapNodes;
    }
    private void OnEnable()
    {
        GameEvents.ChangeInputAuthorityToNpc += OnChangeInputAuthorityToNpc;
        GameEvents.PlayerMadeASelection += OnCheckPlayerChoice;
        _isPerforming = false;
        _canMove = true;
        _currentStage = Stage.Stage1;
        //IsDead = false;

    }
    private void OnDisable()
    {
        GameEvents.ChangeInputAuthorityToNpc -= OnChangeInputAuthorityToNpc;
        GameEvents.PlayerMadeASelection -= OnCheckPlayerChoice;
        _isPerforming = false;
        _canMove = false;
    }

    private void OnChangeInputAuthorityToNpc()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (_isPerforming || !_canMove)
        {
            return;
        }
        CheckStage();
    }

    public enum Stage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4
    }

    private void NextStage()
    {
        _currentStage++;
    }

    private void CheckStage()
    {
        if (_currentStage == Stage.Stage1)
        {
            PerformStage1();
        }
        else if (_currentStage == Stage.Stage2)
        {
            PerformStage2();
        }
        else if (_currentStage == Stage.Stage3)
        {
            PerformStage3();
        }
        else if (_currentStage == Stage.Stage4)
        {
            PerformStage4();
        }
    }

    private void PerformStage1()
    {
        _isPerforming = true;
        MoveToCenter();
        GameEvents.PlaySound("Footstep");
    }

    private void PerformStage2()
    {
        _isPerforming = true;

        WorldUIManager.instance.ShowSpeechBubble(NpcData.Dialogues);
        //Debug.Log("NPC merkeze ula�t� ve bilgi veriyor.");

        
        DOVirtual.DelayedCall(1f, () =>
        {
            NextStage();
            _isPerforming = false;
            _canMove = false;
            GameEvents.ChangeInputAuthorityToPlayer?.Invoke();
        });
    }

    private void PerformStage3()
    {
        _isPerforming = true;
        WorldUIManager.instance.ShowSecondStageUI(NpcData.DesiredIsland);

        NextStage();
        _isPerforming = false;
        _canMove = false;
        GameEvents.ChangeInputAuthorityToPlayer?.Invoke();
    }

    private void PerformStage4()
    {
        _isPerforming = true;
        GameEvents.PlaySound("Footstep");
        MoveToEnd();
    }

    public void MoveToCenter()
    {
        transform.DOJump(_centerTarget, _jumpPower, _numJumps, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                //Debug.Log("NPC merkeze ula�t� ve durdu.");
                NextStage();
                _isPerforming = false; // Animasyon bitti, Update d�ng�s� devam edebilir
                _canMove = false;
                GameEvents.ChangeInputAuthorityToNpc?.Invoke();
            });

    }

    private void MoveToEnd()
    {
        transform.DOJump(_endTarget, _jumpPower, _numJumps, _duration)
           .SetEase(Ease.Linear)
           .OnComplete(() =>
           {
               // E�er sahnede ba�ka bir a�ama yoksa veya obje yok edilecekse i�lemleri buraya ekle
               _isPerforming = false;
               _canMove = false;
               GameEvents.ChangeInputAuthorityToNpc?.Invoke();
               OnNpcFinished?.Invoke();
               //this.gameObject.SetActive(false);
           });
    }
    public void Die()
    {
        IsDead = true;
    }
    //oyuncu secim yapinca event ile bu fonksiyonu cagir
    private void OnCheckPlayerChoice(Direction direction1, Direction direction2)
    {
        var (isDead, island1, island2) = _map.ExploreDirections(direction1, direction2);
        if (isDead)
        {
            Die();
        }
        else
        {
            NpcData.Dialogues.islandOnDirection1 = island1;
            NpcData.Dialogues.islandOnDirection2 = island2;
            NpcData.LastVisitedIsland = island2;
            NpcData.Dialogues.direction1 = direction1;
            NpcData.Dialogues.direction2 = direction2;
        }
    }
}
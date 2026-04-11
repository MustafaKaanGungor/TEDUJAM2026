using DG.Tweening;
using System;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [HideInInspector] public NpcData NpcData;
    private SpriteRenderer _spriteRenderer;
    private Stage _currentStage;
    private bool _isPerforming = true;
    private bool _canMove = true;
    [Header("Jump Settings")]
    //[SerializeField] private Vector3 _spawnTransform;
    [SerializeField] private Vector3 _centerTarget;
    [SerializeField] private float _jumpPower = 2f;
    [SerializeField] private int _numJumps = 1;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private Vector3 _endTarget;

    public void Initialize(NpcData npcData)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        NpcData = npcData;
        _spriteRenderer.sprite = NpcData.NpcSprite;
    }
    private void OnEnable()
    {
        GameEvents.ChangeInputAuthorityToNpc += OnChangeInputAuthorityToNpc;

    }
    private void OnDisable()
    {
        GameEvents.ChangeInputAuthorityToNpc -= OnChangeInputAuthorityToNpc;
    }

    private void OnChangeInputAuthorityToNpc()
    {
        _canMove = true;
    }

    void Start()
    {
        _isPerforming = false;
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
    }

    private void PerformStage2()
    {
        _isPerforming = true;
        Debug.Log("NPC merkeze ulaþtý ve bilgi veriyor.");

        // Diyalog simülasyonu için 1 saniye bekletme eklendi
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
        Debug.Log("NPC bilgi verdi ve gitmek istediði yeri söylüyor.");

        // Diyalog simülasyonu için 1 saniye bekletme eklendi
        DOVirtual.DelayedCall(1f, () =>
        {
            NextStage();
            _isPerforming = false;
            _canMove = false;
            GameEvents.ChangeInputAuthorityToPlayer?.Invoke();
        });
    }

    private void PerformStage4()
    {
        _isPerforming = true;
        MoveToEnd();
    }

    public void MoveToCenter()
    {
        transform.DOJump(_centerTarget, _jumpPower, _numJumps, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("NPC merkeze ulaþtý ve durdu.");
                NextStage();
                _isPerforming = false; // Animasyon bitti, Update döngüsü devam edebilir
                _canMove = false;
                GameEvents.ChangeInputAuthorityToPlayer?.Invoke();
            });
    }

    private void MoveToEnd()
    {
        transform.DOJump(_endTarget, _jumpPower, _numJumps, _duration)
           .SetEase(Ease.Linear)
           .OnComplete(() =>
           {
               Debug.Log("NPC hedefe ulaþtý ve durdu.");
               // Eðer sahnede baþka bir aþama yoksa veya obje yok edilecekse iþlemleri buraya ekle
               _isPerforming = false;
               _canMove = false;
               GameEvents.ChangeInputAuthorityToPlayer?.Invoke();
           });
    }
}
using UnityEngine;
using DG.Tweening;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] private Vector3 _pointA;
    [SerializeField] private Vector3 _pointB;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private Ease _easeType = Ease.Linear;

    private void Start()
    {
        transform.position = _pointA;
        MoveLoop();
    }

    private void MoveLoop()
    {
        var tween = DOTween.Sequence(transform);
        tween.Append(transform.DOMove(_pointB, _duration).SetEase(_easeType));
        tween.Append(transform.DOScaleX(-2, 0.1f));
        tween.Append(transform.DOMove(_pointA, _duration).SetEase(_easeType));
        tween.Append(transform.DOScaleX(2, 0.1f));
        tween.SetLoops(-1);
    }
}

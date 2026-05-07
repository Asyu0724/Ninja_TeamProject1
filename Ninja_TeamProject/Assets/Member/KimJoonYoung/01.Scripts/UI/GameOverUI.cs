using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // [SerializeField] private Image image;
    private Sequence _sequence;

    private void Start()
    {
        UIManager.Instance.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        _sequence =  DOTween.Sequence();
        _sequence.Prepend(transform.DOLocalMoveY(0, 1.5f).SetEase(Ease.OutBounce));
    }
}

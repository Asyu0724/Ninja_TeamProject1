using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    private Sequence _sequence;

    private void Start()
    {
        UIManager.Instance.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        _sequence = DOTween.Sequence();
        _sequence.Prepend(transform.DOLocalMoveY(0, 1.5f).SetEase(Ease.OutBounce));
        _sequence.AppendInterval(0.5f);
        _sequence.OnComplete(GameSelectMenu);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    private void GameSelectMenu()
    {
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }
}

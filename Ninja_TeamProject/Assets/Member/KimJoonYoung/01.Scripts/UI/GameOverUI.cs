using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject EscPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    private Sequence _sequence;

    private void Start()
    {
        UIManager.Instance.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        EscPanel.SetActive(false);
        _sequence = DOTween.Sequence();
        _sequence.Prepend(transform.DOLocalMoveY(0, 1.5f).SetEase(Ease.InOutCubic));
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

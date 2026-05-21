using System;
using System.Collections;
using DG.Tweening;
using Member.KimJoonYoung._01.Scripts.Hp;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field:SerializeField] public HealthBarUI HealthUI {  get; private set; }
    [field: SerializeField] public HealthBarUI BossHealthUI { get; private set; }
    [SerializeField] private Image gameOverImage;
    public event Action OnGameOver;

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void Dead()
    {
        Sequence seq = DOTween.Sequence();
        gameOverImage.gameObject.SetActive(true);
        seq.Prepend(gameOverImage.DOFade(1f, 2f));
        seq.OnComplete(GameOver);
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();

    }
}


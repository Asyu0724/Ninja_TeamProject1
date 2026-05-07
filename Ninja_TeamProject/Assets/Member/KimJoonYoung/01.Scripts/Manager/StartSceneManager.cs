using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Image blackScreenImage;
    public event Action OnSceneChange;

    private void Awake()
    {
        OnSceneChange += HandlerBlackScreenModify;
    }

    private void Start()
    {
        OnSceneChange?.Invoke();
    }
    
    private void HandlerBlackScreenModify()
    {
        Sequence seq = DOTween.Sequence();
        blackScreenImage.gameObject.SetActive(true);
        seq.Append(blackScreenImage.DOFade(0f, 3f).SetEase(Ease.Linear));
    }
}

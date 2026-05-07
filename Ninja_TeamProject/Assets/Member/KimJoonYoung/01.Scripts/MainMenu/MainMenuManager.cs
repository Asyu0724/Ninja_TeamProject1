using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image sceneChangeImage;
    [SerializeField] private GameObject gameStartBtn;
    [SerializeField] private GameObject settingBtn;
    [SerializeField] private GameObject gameBtn;
    
    public static MainMenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        StartCoroutine(SceneChange());
        Sequence s = DOTween.Sequence();
        s.PrependInterval(0.1f);
        s.Append(sceneChangeImage.transform.DOLocalMoveY(1350, 0.5f).SetEase(Ease.InOutCubic));
        s.AppendInterval(0.5f);
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(1);
    }
}

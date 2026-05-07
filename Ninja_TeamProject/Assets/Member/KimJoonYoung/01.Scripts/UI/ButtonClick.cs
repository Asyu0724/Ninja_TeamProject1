using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    public void OnClick()
    {
        Sequence seq = DOTween.Sequence();
        seq.Prepend(_btn.transform.DOScale(new Vector3(1.1f,1.1f,1f),1).SetEase(Ease.OutCubic));
    }
}

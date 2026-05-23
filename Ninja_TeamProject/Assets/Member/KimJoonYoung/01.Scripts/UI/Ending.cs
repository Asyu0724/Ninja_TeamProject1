using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _talninimage;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _text1;
    [SerializeField] private TextMeshProUGUI _text2;
    [SerializeField] private TextMeshProUGUI _text3;
    public UnityEvent OnEnd;
    public void OnCredit()
    {
        Sequence seq = DOTween.Sequence();
        seq.PrependInterval(1f);
        seq.Append(_text.DOFade(0f, 2f));
        seq.Join(_text1.DOFade(0f, 2f));
        seq.Join(_text2.DOFade(0f, 2f));
        seq.Append(_text3.DOFade(0.5f, 2f));
        seq.AppendInterval(1f);
        seq.Append(_text3.DOFade(0f, 2f));
        seq.Append(_talninimage.DOFade(1,1).SetEase(Ease.Linear));
        seq.Append(_image.DOFade(1,4).SetEase(Ease.Linear));
        seq.OnComplete(OnEnd.Invoke);
    }
}

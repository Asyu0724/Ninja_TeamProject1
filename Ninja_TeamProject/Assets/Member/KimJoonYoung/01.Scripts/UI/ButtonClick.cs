using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI
{
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
            seq.SetUpdate(true);
            seq.Prepend(_btn.transform.DOScale(new Vector3(1.1f,1.1f,1f),0.3f).SetEase(Ease.OutCubic));
            seq.Append(_btn.transform.DOScale(new Vector3(1,1,1), 0.2f).SetEase(Ease.OutCubic));
        }

    }
}

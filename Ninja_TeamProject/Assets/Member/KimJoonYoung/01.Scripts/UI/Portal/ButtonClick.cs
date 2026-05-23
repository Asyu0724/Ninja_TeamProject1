using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI
{
    public class ButtonClick : MonoBehaviour , IPointerEnterHandler
    {
        private Button _btn;

        private void Awake()
        {
            _btn = GetComponent<Button>();
        }

        public void OnClick()
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Majestic , 10);
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.Prepend(_btn.transform.DOScale(new Vector3(1.1f,1.1f,1f),0.3f).SetEase(Ease.OutCubic));
            seq.Append(_btn.transform.DOScale(new Vector3(1,1,1), 0.2f).SetEase(Ease.OutCubic));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Tick , 11);
        }
    }
}

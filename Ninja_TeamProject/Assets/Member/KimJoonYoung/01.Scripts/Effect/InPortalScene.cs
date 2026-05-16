using DG.Tweening;
using Member.KimJoonYoung._01.Scripts.UI.Portal;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.Effect
{
    public class InPortalScene : MonoBehaviour
    {
        [SerializeField] private Image blackScreenImage;
        private void Start()
        {
            PortalButtonAction.Instance.OnButtonPress += HandlerChangeTransform;
        }

        private void HandlerChangeTransform()
        {
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.PrependInterval(1f);
            seq.Prepend(transform.DOScale(new Vector3(2f, 2f, -2), 1f).SetEase(Ease.OutCubic));
            seq.Append(blackScreenImage.DOFade(1f, 3f).SetEase(Ease.InOutCubic));
        }
    }
}

    using System;
    using DG.Tweening;
    using Member.KimJoonYoung._01.Scripts.UI;
    using Member.KimJoonYoung._01.Scripts.UI.Portal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.Effect
{
    public class InPortalScene : MonoBehaviour
    {
        [SerializeField] private GameObject sceneChange;
        [SerializeField] private Image blackScreenImage;
        public UnityEvent onEvent;

        private void Start()
        {
            PortalButtonAction.Instance.OnButtonPress += HandlerChangeTransform;
        }

        private void HandlerChangeTransform()
        {
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.PrependInterval(0.5f);
            seq.Prepend(transform.DOScale(new Vector3(2f, 2f, -2), 1f).SetEase(Ease.OutCubic));
            seq.OnPlay(Event);
            seq.Append(blackScreenImage.DOColor(Color.green,0).SetEase(Ease.OutCubic));
            seq.AppendInterval(0.05f);
            seq.Append(blackScreenImage.DOColor(Color.deepPink,0).SetEase(Ease.OutCubic));
            seq.AppendInterval(0.05f);
            seq.Append(blackScreenImage.DOColor(Color.white, 0).SetEase(Ease.OutCubic));
            seq.AppendInterval(0.05f);
            seq.Append(blackScreenImage.DOColor(Color.black, 0).SetEase(Ease.OutCubic));
            seq.AppendInterval(1.5f);
            seq.OnComplete(sceneChange.GetComponent<SceneChangeManager>().ChangeScene);
        }

        private void Event()
        {
            onEvent?.Invoke();
        }
}
}

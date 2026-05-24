using System;
using System.Text;
using DG.Tweening;
using Member.KimJoonYoung._01.Scripts.UI;
using Member.KimJoonYoung._01.Scripts.UI.Portal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.Effect
{
    public class InPortalScene : MonoBehaviour
    {
        [SerializeField] private SceneChangeManager _sceneChangeManager;
        [SerializeField] private Image blackScreenImage;
        public UnityEvent OnEvent;
        [field:SerializeField] public bool OnFlash { get; set; }

        public void CrackSound()
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Crack, 16);
        }
        private void Start()
        {
            if (PlayerPrefs.GetInt("TransitionFlash", 1) == 1)
                OnFlash = true;
            else
                OnFlash = false;
            if (PortalButtonAction.Instance != null)
                PortalButtonAction.Instance.OnButtonPress += HandlerChangeTransform;
        }

        public void ErrorScene()
        {
            if (OnFlash)
            {
                Sequence seq = DOTween.Sequence();
                seq.SetUpdate(true);
                seq.Append(blackScreenImage.DOColor(Color.green, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(0.05f);
                seq.Append(blackScreenImage.DOColor(Color.deepPink, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(0.05f);
                seq.Append(blackScreenImage.DOColor(Color.white, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(0.05f);
                seq.Append(blackScreenImage.DOColor(Color.black, 0).SetEase(Ease.OutCubic));
                seq.Append(blackScreenImage.DOFade(0, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(1.5f);
            }
            else
            {
                Sequence seq = DOTween.Sequence();
                seq.SetUpdate(true);
                seq.Append(blackScreenImage.DOColor(Color.black, 0).SetEase(Ease.OutCubic));
                seq.Append(blackScreenImage.DOFade(0, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(1.65f);
            }
        }
    

        private void HandlerChangeTransform()
        {
            if (OnFlash)
            {
                Sequence seq = DOTween.Sequence();
                seq.SetUpdate(true);
                seq.PrependInterval(0.5f);
                seq.Prepend(transform.DOScale(new Vector3(2f, 2f, -2), 1f).SetEase(Ease.OutCubic));
                seq.OnPlay(Event);
                seq.Append(blackScreenImage.DOFade(1, 0).SetEase(Ease.OutCubic));
                seq.Append(blackScreenImage.DOColor(Color.green, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(0.05f);
                seq.Append(blackScreenImage.DOColor(Color.deepPink, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(0.05f);
                seq.Append(blackScreenImage.DOColor(Color.white, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(0.05f);
                seq.Append(blackScreenImage.DOColor(Color.black, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(1.5f);
                seq.OnComplete(_sceneChangeManager.ChangeScene);
            }
            else
            {
                Sequence seq = DOTween.Sequence();
                seq.SetUpdate(true);
                seq.PrependInterval(0.5f);
                seq.Prepend(transform.DOScale(new Vector3(2f, 2f, -2), 1f).SetEase(Ease.OutCubic));
                seq.OnPlay(Event);
                seq.Append(blackScreenImage.DOColor(Color.black, 0).SetEase(Ease.OutCubic));
                seq.AppendInterval(1.65f);
                seq.OnComplete(_sceneChangeManager.ChangeScene);
            }
        }

        private void Event()
        {
            OnEvent?.Invoke();
        }
    }
}

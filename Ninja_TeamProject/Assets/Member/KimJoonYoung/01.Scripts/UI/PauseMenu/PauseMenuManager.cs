using System;
using DG.Tweening;
using Member.KimJoonYoung._01.Scripts.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI.PauseMenu
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Button rsmBtn;
        [SerializeField] private Button settingBtn;
        [SerializeField] private Button exitBtn;
        private Image _panel;
        private bool _isPaused;
        private bool _canOnOff = true;
        
        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame && _canOnOff)
            {
                if (!_isPaused)
                    OnPause();
                else
                    OffPause();
            }
        }

        private void Awake()
        {
            _panel = GetComponent<Image>();
        }
        
        public void OffPause()
        {
            Sequence seq = DOTween.Sequence();
            _isPaused = false;
            _canOnOff = false;
            playerInput.enabled = true;
            TimeScaleManager.Instance.TimeResume();
            OffPauseMove(seq);
        }

        private void OffPauseMove(Sequence seq)
        {
            seq.SetUpdate(true);
            seq.Prepend(_panel.DOFade(0f, 0.5f).SetEase(Ease.OutCubic).SetUpdate(true));
            seq.Join(title.transform.DOLocalMoveX(-1400.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(rsmBtn.transform.DOLocalMoveX(-1400.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(settingBtn.transform.DOLocalMoveX(-1400.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(exitBtn.transform.DOLocalMoveX(-1400.0f, 0.5f).SetEase(Ease.OutCubic));
            seq.OnComplete(CanOnMenu);
        }
        
        private void OnPauseMove(Sequence seq)
        {
            seq.SetUpdate(true);
            seq.Prepend(_panel.DOFade(0.8f, 0.5f).SetEase(Ease.OutCubic));
            seq.Join(title.transform.DOLocalMoveX(-750.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(rsmBtn.transform.DOLocalMoveX(-750.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(settingBtn.transform.DOLocalMoveX(-750.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(exitBtn.transform.DOLocalMoveX(-750.0f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.OnComplete(CanOnMenu);
        }

        public void OnPause()
        {
            _isPaused = true;
            _canOnOff = false;
            playerInput.enabled = false;
            TimeScaleManager.Instance.TimeStop();
            Sequence seq = DOTween.Sequence();
            OnPauseMove(seq);
        }
        
        private void CanOnMenu()
        {
            _canOnOff = true;
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}
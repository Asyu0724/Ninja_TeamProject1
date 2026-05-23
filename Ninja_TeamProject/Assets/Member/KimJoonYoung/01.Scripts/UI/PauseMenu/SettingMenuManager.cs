using DG.Tweening;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.UI.PauseMenu
{
    public class SettingMenuManager : MonoBehaviour
    {
        private bool _isOnSetting;
        [SerializeField] private RectTransform sfx;
        [SerializeField] private RectTransform bgm;

        public void Setting()
        {
            if (!_isOnSetting)
                OnSetting();
            else
                OffSetting();
        }
        
        public void OnSetting()
        {
            _isOnSetting = true;
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.Prepend(sfx.DOLocalMoveX(675f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(bgm.DOLocalMoveX(675f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
        }
        public void OffSetting()
        {
            _isOnSetting = false;
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.Prepend(sfx.DOLocalMoveX(1250f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
            seq.Join(bgm.DOLocalMoveX(1250f, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.05f));
        }

    }
}

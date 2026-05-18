using System;
using System.Collections;
using DG.Tweening;
using Member.KimJoonYoung._01.Scripts.Manager;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace Member.KimJoonYoung._01.Scripts.UI
{
    public class SceneChangeManager : MonoBehaviour
    {
        [SerializeField] private int sceneNumber;
        private Image _sceneChangeImage;
        private bool _changingNow = false;

        private void Awake()
        {
            _sceneChangeImage = GetComponent<Image>();
        }

        public void ChangeScene()
        {
            if (_changingNow) return;
            _changingNow = true;
            StartCoroutine(SceneChange());
            Sequence s = DOTween.Sequence();
            s.SetUpdate(true);
            s.SetLink(gameObject);
            s.PrependInterval(0.1f);
            s.Append(_sceneChangeImage.transform.DOLocalMoveY(1350, 0.5f).SetEase(Ease.InOutCubic));
            s.AppendInterval(0.5f);
        }

        IEnumerator SceneChange()
        {
            yield return new WaitForSecondsRealtime(1.1f);
            DOTween.KillAll();
            SceneManager.LoadScene(sceneNumber);
            TimeScaleManager.Instance?.TimeResume();
        }
    }
}

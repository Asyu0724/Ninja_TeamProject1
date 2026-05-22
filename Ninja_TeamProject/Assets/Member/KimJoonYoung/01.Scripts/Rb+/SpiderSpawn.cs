using System;
using System.Collections;
using DG.Tweening;
using Member.Choijeongyun._01.Scripts.Func;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

namespace Member.KimJoonYoung._01.Scripts.Rb_
{
    public class SpiderSpawn : MonoBehaviour
    {
        [SerializeField] private CJY_AudioManager bossAudio;
        [SerializeField] private GameObject web;
        private BossMover _bossMover;
        private BossHealth _bossHealth;
        private BossRenderer _bossRenderer;
        private BossSkill _bossSkill;
        public event Action OnBossSpawn;
        public UnityEvent OnSystemEvent;
        public UnityEvent OnFallEvent;

        private void Awake()
        {
            _bossMover = GetComponent<BossMover>();
            _bossHealth = GetComponent<BossHealth>();
            _bossRenderer = GetComponentInChildren<BossRenderer>();
            _bossSkill = GetComponentInChildren<BossSkill>();
        }

        private void Start()
        {
            Sequence seq = DOTween.Sequence();
            seq.Prepend(transform.DOLocalMoveY(transform.position.y - 7f, 3f).SetEase(Ease.OutQuart));
            seq.OnComplete(InBossScene);
        }

        private void InBossScene()
        {
            Sequence seq = DOTween.Sequence();
            _bossMover.enabled = true;
            OnBossSpawn?.Invoke();
            seq.Prepend(web.transform.DOLocalMoveY(transform.position.y + 20f, 2f).SetEase(Ease.OutCubic).OnPlay(WaitFallStart));
            seq.AppendInterval(2f);
            seq.OnComplete(OnBossSystem);
        }

        private void OnBossSystem()
        {
            OnSystemEvent?.Invoke();
            _bossMover.enabled = true;
            _bossHealth.enabled = true;
            _bossRenderer.enabled = true;
            _bossSkill.enabled = true;
        }

        private void WaitFallStart()
        {
            StartCoroutine(WaitFallTime());
        }
        
        private IEnumerator WaitFallTime()
        {
            yield return new WaitForSeconds(1f);
            bossAudio.PlaySFX(2,0);
            OnFallEvent?.Invoke();
        }
    }
}

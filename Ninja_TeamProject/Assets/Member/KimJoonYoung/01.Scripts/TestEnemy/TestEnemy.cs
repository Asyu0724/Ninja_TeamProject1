using System.Collections;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.TestEnemy
{
    public class TestEnemy : global::Agent
    {
        private HealthSystem _healthSystem;
        private EnemyHealthBarUI _enemyHealthBarUI;
        private bool _attacked;

        // Hash
        private readonly int _isAttackedHash = Animator.StringToHash("Attacked");

        protected override void Awake()
        {
            base.Awake();
            _healthSystem = GetComponent<HealthSystem>();
            _enemyHealthBarUI = GetComponentInChildren<EnemyHealthBarUI>();
            _healthSystem.OnDamaged += AttackedNow;
        }

        private void Update()
        {
            _agentRenderer.SetBoolParam(_isAttackedHash, _attacked);
        }

        private void AttackedNow()
        {
            _enemyHealthBarUI.HealthBarUpdate();
            _attacked = true;
            StartCoroutine(Attacked());
        }

        IEnumerator Attacked()
        {
            yield return new WaitForSeconds(0.2f);
            _attacked = false;
        }
    }
}

using System.Collections;
using Member.KimJoonYoung._01.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.KimJoonYoung._01.Scripts.Agent
{
    public class PlayerSkill : global::Agent
    {
        private PlayerController _playerController;
        private bool _playerHited;

        // Attack Setting
        [Header("ComboSetting")]
        [SerializeField] private float canComboAttackTimer;
        [SerializeField] private float canAttackTimer;
        private int _currentAttackComboCount;
        private int _attackComboCount;
        private bool _canComboAttack = true;
        private bool _canAttack = true;

        // QSKill Setting
        [Header("QSkillSetting")]
        public float qSkillCoolTime;
        public bool QSkillUse { get; private set; }
        private bool _qSkillCoolTime = true;
        private bool _qSkill;
        [SerializeField] private int qSkillDamageAmount = 3;

        // Hash
        private int _attackComboCountHash = Animator.StringToHash("AttackComboCount");
        private int _qSkillHash = Animator.StringToHash("QSkill");
        private int _qSkillUseHash = Animator.StringToHash("QSkillUse");

        private PlayerSkillBarUI _playerSkillBarUI;

        protected override void Awake()
        {
            base.Awake();
            _playerSkillBarUI = GetComponentInChildren<PlayerSkillBarUI>();
            _playerController = GetComponent<PlayerController>();
        
        }


        private void Update()
        {
            _agentRenderer.SetIntegerParam(_attackComboCountHash, _currentAttackComboCount);
            _agentRenderer.SetBoolParam(_qSkillHash, _qSkill);
            _agentRenderer.SetBoolParam(_qSkillUseHash, QSkillUse);

            if (_attackComboCount > 1)
            {
                _attackComboCount = 0;
                _canAttack = false;
                StartCoroutine(AttackTimer()); // 콤보 끝나면 0.5초 기다리셈
            }

            _playerHited = _playerController.PlayerHit;
        }


        private void OnAttack(InputValue value)
        {
            if (!_playerHited && !QSkillUse && _canAttack && _canComboAttack)
            {
                _currentAttackComboCount = ++_attackComboCount;
                _canComboAttack = false;
                StartCoroutine(AttackComboTimer());
                StartCoroutine(AttackCombo());
            }
        }
        private void OnSkill(InputValue value)
        {
            if (!_playerHited && _canAttack && _qSkillCoolTime && _agentMover.isGrounded)
            {
                _qSkill = true;
                QSkillUse = true;
                _qSkillCoolTime = false;
                StartCoroutine(UseQSkill());
                StartCoroutine(QSkillAttack());
                StartCoroutine(CanQSkill());
                StartCoroutine(QSkillCoolTime());
                _playerSkillBarUI.QSkillCoolTimeBarUpdate();
            }
        }


        private void AttackNow()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.GetDamage(qSkillDamageAmount, gameObject);
            }
        }
        private void QSkillNow()
        {
            SkillOverlab();
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.GetDamage(qSkillDamageAmount, gameObject);
            }
            SkillOverlabReset();
        }

        private void SkillOverlab()
        {
            _agentAttack.SkillBoxSize(new Vector2(5,_agentAttack.boxSize.y));
            _agentAttack.SkillOffset(new Vector2(1.9f,_agentAttack.offset.y));
        }

        private void SkillOverlabReset()
        {
            _agentAttack.FirstBoxSize();
            _agentAttack.FirstOffset();
        }

        /*---------------------------------------------------*/ // Coroutine
        IEnumerator AttackTimer()
        {
            while (!_canAttack)
            {
                yield return new WaitForSeconds(canAttackTimer);
                _canAttack = true;
            }
        }
        IEnumerator AttackComboTimer()
        {

            while (!_canComboAttack)
            {
                AttackNow();
                yield return new WaitForSeconds(canComboAttackTimer);
                _canComboAttack = true;
            }
        }
        IEnumerator AttackCombo()
        {
            yield return new WaitForSeconds(0);
            _currentAttackComboCount = 0;
        }
        IEnumerator UseQSkill()
        {
            _agentMover._rb.linearVelocityX = 0f;
            yield return new WaitForSeconds(1f);
            QSkillUse = false;
        }
        IEnumerator QSkillAttack()
        {
            yield return new WaitForSeconds(0.5f);
            QSkillNow();
        }
        IEnumerator CanQSkill()
        {
            yield return new WaitForSeconds(0f);
            _qSkill = false;
        }
        IEnumerator QSkillCoolTime()
        {
            yield return new WaitForSeconds(qSkillCoolTime);
            _qSkillCoolTime = true;
        }
    }
}

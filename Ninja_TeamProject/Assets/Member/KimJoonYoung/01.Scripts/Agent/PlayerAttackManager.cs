using System.Collections;
using Member.KimJoonYoung._01.Scripts.Player;
using Member.KimJoonYoung._01.Scripts.SO;
using Member.KimJoonYoung._01.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Member.KimJoonYoung._01.Scripts.Agent
{
    public class PlayerAttackManager : global::Agent
    {
        [SerializeField] private LayerMask damageLayerMask;
        private PlayerController _playerController;
        private bool _playerHited;

        // Attack Setting
        [field:SerializeField] public PlayerAttackDataSO PlayerAttackData { get; private set; }
        private float _canComboAttackTimer;
        private float _canAttackTimer;
        private int _attackDamageAmount = 1;
        private int _currentAttackComboCount;
        private int _attackComboCount;
        private bool _canComboAttack = true;
        private bool _canAttack = true;

        // QSKill Setting
        [field:SerializeField] public PlayerSkillDataSO PlayerSkillData { get; private set; }
        [SerializeField] private TextMeshProUGUI cantUseSkillText;

        private int _qSkillDamageAmount;
        private Vector2 _qSkillBoxSize;
        private Vector2 _qSkillOffset;
        private bool _qSkill;
        private bool _canUseQSkill;
        public float QSkillCoolTimeValue { get; private set; }
        public bool QSkillCoolTimeNow { get; private set;} = true;
        public bool QSkillUse { get; private set; }

        // Hash
        private int _attackComboCountHash = Animator.StringToHash("AttackComboCount");
        private int _qSkillHash = Animator.StringToHash("QSkill");
        private int _qSkillUseHash = Animator.StringToHash("QSkillUse");

        private PlayerSkillBarUI _playerSkillBarUI;

        protected override void Awake()
        {
            base.Awake();
            _attackDamageAmount = PlayerAttackData.attackDamageAmount;
            _canAttackTimer = PlayerAttackData.canAttackTimer;
            _canComboAttackTimer = PlayerAttackData.canComboAttackTimer;
        
            _qSkillDamageAmount = PlayerSkillData.skillDamageAmount;
            _qSkillBoxSize = PlayerSkillData.skillBoxSize;
            _qSkillOffset = PlayerSkillData.skillBoxOffset;
            QSkillCoolTimeValue = PlayerSkillData.skillCoolTime;
        
            cantUseSkillText.text = null;
        
            _playerSkillBarUI = GetComponentInChildren<PlayerSkillBarUI>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (!_playerController.PlayerIsDead)
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
        }


        private void OnAttack(InputValue value)
        {
            if (!_playerHited && !QSkillUse && _canAttack && _canComboAttack && !_playerController.PlayerIsDead)
            {

                switch (Random.Range(0, 3))
                {
                    case 0:
                        AudioManager.instance.PlaySfx(AudioManager.Sfx.avgAtk0);
                        break;
                    case 1:
                        AudioManager.instance.PlaySfx(AudioManager.Sfx.avgAtk1);
                        break;
                    case 2:
                        AudioManager.instance.PlaySfx(AudioManager.Sfx.avgAtk2);
                        break;
                }

                _currentAttackComboCount = ++_attackComboCount;
                _canComboAttack = false;
                StartCoroutine(AttackComboTimer());
                StartCoroutine(AttackCombo());
            }
        }

        private void OnSkill(InputValue value)
        {
            if (!QSkillUse && !_playerController.PlayerIsDead)
            {
                if (!_playerHited && _canAttack && QSkillCoolTimeNow && _agentMover.isGrounded && CheckGround())
                {
                    cantUseSkillText.text = null;
                    _qSkill = true;
                    QSkillUse = true;
                    QSkillCoolTimeNow = false;
                    StartCoroutine(UseQSkill());
                    StartCoroutine(QSkillAttack());
                    StartCoroutine(CanQSkill());
                    StartCoroutine(QSkillCoolTime());
                    StartCoroutine(QSkillAttackSound());
                    _playerSkillBarUI.QSkillCoolTimeBarUpdate();
                }
                else
                {
                    if (cantUseSkillText.text == null)
                        StartCoroutine(QSkillText());
                    _agentAttack.FirstBoxSize();
                    _agentAttack.FirstOffset();
                }
            }
        }


        private void AttackNow()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset,
                _agentAttack.boxSize, 0 , damageLayerMask);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.GetDamage(_attackDamageAmount,gameObject);
            }
        }


        private void QSkillNow()
        {
            _agentAttack.SkillBoxSize(_qSkillBoxSize);
            _agentAttack.SkillOffset(_qSkillOffset);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset,
                _agentAttack.boxSize, 0,damageLayerMask);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.GetDamage(_qSkillDamageAmount, gameObject);
            }
            _agentAttack.FirstBoxSize(); 
            _agentAttack.FirstOffset();
        }

        private bool CheckGround()
        {
            _agentAttack.SkillBoxSize(_qSkillBoxSize);
            _agentAttack.SkillOffset(_qSkillOffset);
            Collider2D[] collider2Ds =
                Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.gameObject.CompareTag("Ground"))
                    return false;
            }
            return true;
        }

        /*---------------------------------------------------*/ // Coroutine
        IEnumerator AttackTimer()
        {
            while (!_canAttack)
            {
                yield return new WaitForSeconds(_canAttackTimer);
                _canAttack = true;
            }
        }
        IEnumerator AttackComboTimer()
        {

            while (!_canComboAttack)
            {
                AttackNow();
                yield return new WaitForSeconds(_canComboAttackTimer);
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
            _agentAttack.FirstBoxSize();
            _agentAttack.FirstOffset();
        }
        IEnumerator QSkillAttack()
        {
            yield return new WaitForSeconds(0.5f);
            QSkillNow();
        }
        IEnumerator QSkillAttackSound()
        {
            yield return new WaitForSeconds(0.3f);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.QSkill); 
        }
        IEnumerator CanQSkill()
        {
            yield return new WaitForSeconds(0f);
            _qSkill = false;
        }
        IEnumerator QSkillCoolTime()
        {
            yield return new WaitForSeconds(QSkillCoolTimeValue);
            QSkillCoolTimeNow = true;
        }

        IEnumerator QSkillText()
        {
            cantUseSkillText.text = "Can't Use Q Skill";
            yield return new WaitForSeconds(3);
            cantUseSkillText.text = null;
        }
    
    }
}
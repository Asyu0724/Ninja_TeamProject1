using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
using System.Linq.Expressions;
using TMPro;

public class PlayerSkill : Agent
{
    private PlayerController _playerController;
    private PlayerSkillBarUI _playerSkillBarUI;
    private bool _playerHited;

    // Attack Setting
    [Header("AttackSetting")]
    [SerializeField] private float _canComboAttackTimer;
    [SerializeField] private float _canAttackTimer;
    [SerializeField] private int _averageAttack;
    private int _currentAttackComboCount;
    private int _attackComboCount;
    private bool _canComboAttack = true;
    private bool _canAttack = true;

    // QSKill Setting
    [Header("QSkillSetting")]
    [SerializeField] private TextMeshProUGUI _qSkillCantUseText;
    [SerializeField] private int _qSkillDamageAmount;
    [SerializeField] private Vector2 _qSkillSize;
    [SerializeField] private Vector2 _qSkillOffset;

    public bool _qSkillUse { get; private set; }
    public float _qskillCoolTime;
    private bool _qSkillCoolTime = true;
    private bool _qSkill;
    private bool _canUseQSkill;


    // Hash
    private int _attackComboCountHash = Animator.StringToHash("AttackComboCount");
    private int _qSkillHash = Animator.StringToHash("QSkill");
    private int _qSkillUseHash = Animator.StringToHash("QSkillUse");


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
        _agentRenderer.SetBoolParam(_qSkillUseHash, _qSkillUse);

        if (_attackComboCount > 1)
        {
            _attackComboCount = 0;
            _canAttack = false;
            StartCoroutine(AttackTimer()); // 콤보 끝나면 0.5초 기다리셈
        }

        _playerHited = _playerController._playerHited;
    }


    private void OnAttack(InputValue value)
    {
        if (!_playerHited && !_qSkillUse    )
        {
            if (_canAttack) // 공격 가능 상태일때
            {
                if (_canComboAttack)
                {
                    _currentAttackComboCount = ++_attackComboCount;
                    _canComboAttack = false;
                    StartCoroutine(AttackComboTimer());
                    StartCoroutine(AttackCombo());
                }
            }
        }
    }
    private void OnSkill(InputValue value)
    {
        if (!_playerHited && _canAttack && _qSkillCoolTime && _agentMover.isGrounded)
        {
            _agentAttack.SkillBoxSize(_qSkillSize);
            _agentAttack.SkillOffset(_qSkillOffset);
            CanUseSkillCheck();
            if (_canUseQSkill)
            {
                _qSkillCantUseText.text = null;
                _qSkill = true;
                _qSkillUse = true;
                _qSkillCoolTime = false;
                StartCoroutine(UseQSkill());
                StartCoroutine(QSkillAttack());
                StartCoroutine(CanQSkill());
                StartCoroutine(QSkillCoolTime());
                _playerSkillBarUI.QSkillCoolTimeBarUpdate();
            }
            else
            {
                _agentAttack.FirstBoxSize();
                _agentAttack.FirstOffset();
            }
        }
    }


    private void AttackNow()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.TryGetComponent(out TestEnemy enemy))
            {
                collider.gameObject.GetComponent<HealthSystem>().GetDamage(_averageAttack, gameObject);
                enemy.AttackedNow();
            }
        }

    }
    private void QSkillNow()
    {

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.TryGetComponent(out TestEnemy enemy))
            {

                collider.gameObject.GetComponent<HealthSystem>().GetDamage(_qSkillDamageAmount, gameObject);
                enemy.AttackedNow();
            }
        }

        _agentAttack.FirstBoxSize();
        _agentAttack.FirstOffset();
    }

    private void CanUseSkillCheck()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (!collider.CompareTag("Ground"))
                _canUseQSkill = true;

            else
            {
                _canUseQSkill = false;
                StartCoroutine(QSkillCantUse());
            }
        }
    }

    /*---------------------------------------------------*/ // Coroutine

    IEnumerator QSkillCantUse()
    {

        _qSkillCantUseText.text = ("Can't Use QSkill");
            yield return new WaitForSeconds(3);
        _qSkillCantUseText.text = null;
    }

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
        _qSkillUse = false;
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
        yield return new WaitForSeconds(_qskillCoolTime);
        _qSkillCoolTime = true;
    }
}

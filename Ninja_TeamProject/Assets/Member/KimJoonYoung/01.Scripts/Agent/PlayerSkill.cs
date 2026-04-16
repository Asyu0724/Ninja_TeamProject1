using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerSkill : Agent
{
    private PlayerController playerController;
    private bool _playerHited;

    // Attack Setting
    [Header("ComboSetting")]
    [SerializeField] private float _canComboAttackTimer;
    [SerializeField] private float _canAttackTimer;
    private int _currentAttackComboCount;
    private int _attackComboCount;
    private bool _canComboAttack = true;
    private bool _canAttack = true;

    // QSKill Setting
    [Header("QSkillSetting")]
    public float _qskillCoolTime;
    public bool _qSkillUse { get; private set; }
    private bool _qSkillCoolTime = true;
    private bool _qSkill;
    [SerializeField] private int _qSkillDamageAmount = 3;

    // Hash
    private int _attackComboCountHash = Animator.StringToHash("AttackComboCount");
    private int _qSkillHash = Animator.StringToHash("QSkill");
    private int _qSkillUseHash = Animator.StringToHash("QSkillUse");

    private PlayerSkillBarUI _playerSkillBarUI;

    protected override void Awake()
    {
        base.Awake();
        _playerSkillBarUI = GetComponentInChildren<PlayerSkillBarUI>();
        playerController = GetComponent<PlayerController>();
        
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

        _playerHited = playerController._playerHited;
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
            _qSkill = true;
            _qSkillUse = true;
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
            if (collider.gameObject.TryGetComponent(out TestEnemy enemy))
            {
                collider.gameObject.GetComponent<HealthSystem>().GetDamage(1, gameObject);
                enemy.AttackedNow();
            }
        }

    }
    private void QSkillNow()
    {
        _agentAttack.SkillBoxSize(5);
        _agentAttack.SkillOffset(1.9f);
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

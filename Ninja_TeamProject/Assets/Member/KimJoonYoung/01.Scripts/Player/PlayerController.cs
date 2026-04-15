using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;

public class PlayerController : Agent
{
    // Player Setting
    [Header("PlayerSetting")]
    [SerializeField] private float _speed;
    [SerializeField] private int _jumpPower;
    [SerializeField] private int _jumpCount;

    private int _currentJumpCount;
    private float _moveDir;
    private float _lastMoveDir = 1;
    private bool _isGrounded;
    private bool _playerHited;

    // Health
    private HealthSystem _healthSystem;

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
    private PlayerSkillBarUI _playerSkillBarUI;
    public float _qskillCoolTime;
    public bool _qSkillUse { get; private set; }
    private bool _qSkillCoolTime = true;
    private bool _qSkill;


    // Hash
    private int _xMoveHash = Animator.StringToHash("X_Move");
    private int _yVelocityHash = Animator.StringToHash("Y_Velocity");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _playerHitedHash = Animator.StringToHash("PlayerHited");
    private int _attackComboCountHash = Animator.StringToHash("AttackComboCount");
    private int _qSkillHash = Animator.StringToHash("QSkill");
    private int _qSkillUseHash = Animator.StringToHash("QSkillUse");

    /*---------------------------------------------------*/ // Initialization
    protected override void Awake()
    {
        base.Awake();
        _currentJumpCount = _jumpCount;
        _healthSystem = GetComponent<HealthSystem>();
        _playerSkillBarUI = GetComponentInChildren<PlayerSkillBarUI>();
    }

    private void Start()
    {
        UIManager.Instance.HealthUI.InitHealthUI(_healthSystem.Health);
        _healthSystem.OnDamaged += UpdateHealthUI;
    }

    /*---------------------------------------------------*/ // Physics 

    private void FixedUpdate()
    {
        if (!_playerHited && !_qSkillUse)
        {
            _isGrounded = _agentMover.CheckGround();
            if (_isGrounded && _agentMover._rb.linearVelocityY <= 0)
                _currentJumpCount = _jumpCount;

            _agentMover._rb.linearVelocityX = _moveDir * _speed;

            Flip();
            _agentAttack.Flip(_lastMoveDir);
        }
    }

    /*---------------------------------------------------*/ // Input event

    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>().x;

        if (_moveDir != 0)
            _lastMoveDir = value.Get<Vector2>().x;
    }
    private void OnJump(InputValue value)
    {
        if (!_playerHited && !_qSkillUse)
        {
            if (_currentJumpCount < 1) return;

            if (_currentJumpCount > 0)
            {
                _agentMover.AddForceToAgent(_jumpPower * Vector2.up);
                _currentJumpCount--;
            }
        }
    }

    private void OnAttack(InputValue value)
    {
        if (!_playerHited && !_qSkillUse)
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
        if (!_playerHited && _canAttack && _qSkillCoolTime)
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
    /*---------------------------------------------------*/ // Game logic

    private void Update()
    {
        _agentRenderer.SetIntegerParam(_attackComboCountHash, _currentAttackComboCount);
        _agentRenderer.SetFloatParam(_xMoveHash, Mathf.Abs(_moveDir));
        _agentRenderer.SetFloatParam(_yVelocityHash, _agentMover._rb.linearVelocityY);
        _agentRenderer.SetBoolParam(_isGroundedHash, _isGrounded);
        _agentRenderer.SetBoolParam(_playerHitedHash, _playerHited);
        _agentRenderer.SetBoolParam(_qSkillHash, _qSkill);
        _agentRenderer.SetBoolParam(_qSkillUseHash, _qSkillUse);

        if (_attackComboCount > 1)
        {
            _attackComboCount = 0;
            _canAttack = false;
            StartCoroutine(AttackTimer()); // 콤보 끝나면 0.5초 기다리셈
        }
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

    IEnumerator PlayerHited()
    {
        _playerHited = true;
        _agentMover._rb.linearVelocityX *= 0.3f;
        yield return new WaitForSeconds(0.1f);
        _playerHited = false;
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


    /*---------------------------------------------------*/ // Game method

    private void Flip()
    {
        if (0 != _lastMoveDir)
        {
            transform.rotation = Quaternion.Euler(0, _lastMoveDir > 0 ? 0 : 180f, 0f); // 플립
        }
    }

    private void AttackNow()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)_agentAttack.offset, _agentAttack.boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<HealthSystem>().GetDamage(1,gameObject);
                collider.gameObject.GetComponent<TestEnemy>().AttackedNow();
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
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<HealthSystem>().GetDamage(3, gameObject);
                collider.gameObject.GetComponent<TestEnemy>().AttackedNow();
            }
        }
        _agentAttack.FirstBoxSize();
        _agentAttack.FirstOffset();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(PlayerHited());
        }
    }

    private void UpdateHealthUI()
    {
        UIManager.Instance.HealthUI.UpdateHealthUI(_healthSystem.Health);
    }

    private void OnDestroy()
    {
        _healthSystem.OnDamaged -= UpdateHealthUI;
    }
}

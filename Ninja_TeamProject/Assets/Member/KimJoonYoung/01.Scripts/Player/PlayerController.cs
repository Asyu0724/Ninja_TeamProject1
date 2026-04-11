using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;

public class PlayerControllers : Agent
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

    // Combo Setting
    [Header("ComboSetting")]
    [SerializeField] private float _canComboAttackTimer;
    [SerializeField] private float _canAttackTimer;

    private int _currentAttackComboCount;
    private int _attackComboCount;
    private bool _canComboAttack = true;
    private bool _canAttack = true;

    // Hash
    private int _xMoveHash = Animator.StringToHash("X_Move");
    private int _yVelocityHash = Animator.StringToHash("Y_Velocity");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _AttackComboCountHash = Animator.StringToHash("AttackComboCount");

    /*---------------------------------------------------*/ // Initialization
    protected override void Awake()
    {
        base.Awake();
        _currentJumpCount = _jumpCount;
    }

    /*---------------------------------------------------*/ // Physics 

    private void FixedUpdate()
    {
        _isGrounded = _agentMover.CheckGround();
        if (_isGrounded && _agentMover._rb.linearVelocityY <= 0)
            _currentJumpCount = _jumpCount;

        _agentMover._rb.linearVelocityX = _moveDir * _speed;

        Flip();
        _agentAttack.Flip(_lastMoveDir);
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
        if (_currentJumpCount < 1) return;

        if (_currentJumpCount > 0)
        {
            _agentMover.AddForceToAgent(_jumpPower * Vector2.up);
            _currentJumpCount--;
        }
    }

    private void OnAttack(InputValue value)
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

    /*---------------------------------------------------*/ // Game logic

    private void Update()
    {
        _agentRenderer.SetIntegerParam(_AttackComboCountHash, _currentAttackComboCount);
        _agentRenderer.SetFloatParam(_xMoveHash, Mathf.Abs(_moveDir));
        _agentRenderer.SetFloatParam(_yVelocityHash, _agentMover._rb.linearVelocityY);
        _agentRenderer.SetBoolParam(_isGroundedHash, _isGrounded);

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
            collider.gameObject.GetComponent<TestEnemy>().AttackedNow(collider);
        }
    }

}

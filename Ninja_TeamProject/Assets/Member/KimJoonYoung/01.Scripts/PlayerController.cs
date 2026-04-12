using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;

public class PlayerControllers : MonoBehaviour
{
    [Header("PlayerSettingValue")]
    [SerializeField] private float speed;
    [SerializeField] private float JumpPower;

    [SerializeField] private int _jumpCount;


    [Header("OverLab")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 offset;
    [SerializeField] private LayerMask whatIsGround;


    // Player
    private float _moveDir;
    private float _lastMoveDir;


    private bool _isGrounded;



    // Player 공격
    private int _playerAtkCombo;
    private int _currentJumpCount;
    private float _playerAtkTimer;
    private float _cantMoveTimer;
    private bool _atkTime = true;
    private bool _atkNow = true;


    private Rigidbody2D _rb;
    private Animator _animator;


    // Hash
    private int _xMoveHash = Animator.StringToHash("X_Move");
    private int _yVelocityHash = Animator.StringToHash("Y_Velocity");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _playerAtkHash1 = Animator.StringToHash("PlayerAtk1");
    private int _playerAtkHash2 = Animator.StringToHash("PlayerAtk2");
    private int _playerAtkHash3 = Animator.StringToHash("PlayerAtk3");
    private int _attackNowHash = Animator.StringToHash("AttackNow");


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _currentJumpCount = _jumpCount;
    }

    private void FixedUpdate()
    {
        // 애니메이션
        _animator.SetFloat(_xMoveHash, Mathf.Abs(_moveDir));
        _animator.SetFloat(_yVelocityHash, _rb.linearVelocityY);
        _animator.SetBool(_isGroundedHash, _isGrounded);
        _animator.SetBool(_attackNowHash, _atkNow);

        // 플레이어
        if (_cantMoveTimer < 0)
        {
            _rb.linearVelocityX = _moveDir * speed; // 속도
            transform.localRotation = Quaternion.Euler(0, _lastMoveDir > 0 ? 0 : 180f, 0f); // 플립
        }

        CheckGround();

        if (_cantMoveTimer > 0)
        {
            _rb.linearVelocityX = 0;
            _cantMoveTimer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        if (_isGrounded && Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlayerAtk();
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)offset, boxSize, 0, whatIsGround);
        if (_isGrounded)
        {
            _currentJumpCount = _jumpCount;
        }
    }

    private void PlayerAtk() // 공격 메소드
    {
        StartCoroutine(AtkComboTimeReset());

        if (_atkNow) // 공격 중 일때
        {
            _cantMoveTimer = 0.75f;
            StartCoroutine(AtkCoolDown());
            switch (_playerAtkCombo)
            {
                case 1:
                    _animator.SetTrigger(_playerAtkHash1); // 공격 콤보 1
                    break;
                case 2:
                    _animator.SetTrigger(_playerAtkHash2); // 공격 콤보 2
                    break;
                case 3:
                    _animator.SetTrigger(_playerAtkHash3); // 공격 콤보 3
                    break;
            }
        }
        _atkNow = false;
    }

    IEnumerator AtkComboTimeReset()
    {
        _playerAtkTimer = 1.9f; // n초 안에 콤보를 해야됨
        while (_playerAtkTimer > 0)
        {
            _playerAtkTimer -= Time.deltaTime;
            yield return null;
        }
        _playerAtkCombo = 0;
    }

    IEnumerator AtkCoolDown()
    {
        _playerAtkCombo++;
        yield return new WaitForSeconds(0.4f);
        _atkNow = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, boxSize);
    }


    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>().x;

        if (_moveDir != 0)
        {
            _lastMoveDir = value.Get<Vector2>().x;
        }
    }
    private void OnJump(InputValue value)
    {
        if (_currentJumpCount == 0)
            return;

        if (_currentJumpCount > 0 && _cantMoveTimer < 0)
        {
            _rb.linearVelocityY = 0;
            _rb.AddForceY(JumpPower, ForceMode2D.Impulse);

            _currentJumpCount -= 1;
        }
    }
}

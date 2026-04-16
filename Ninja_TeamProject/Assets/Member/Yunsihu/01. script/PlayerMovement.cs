using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    [Header("PlayerSettingValue")]
    [SerializeField] private float speed;
    [SerializeField] private float JumpPower;

    [SerializeField] private int _jumpCount;


    [Header("OverLab")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 offset;
    [SerializeField] private LayerMask whatIsGround;


    // PlayerMoveController
    private float _moveDir;
    private float _lastMoveDir;


    private bool _isGrounded;



    // PlayerMoveController 공격
    private int _playerAtkCombo;
    private int _currentJumpCount;

    private float _playerAtkTimer;
    private float _playerAtkCoolTime;

    private bool _atkTime = true;
    private bool _atkNow;


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
        _animator.SetFloat(_xMoveHash, Mathf.Abs(_moveDir));
        _animator.SetFloat(_yVelocityHash, _rb.linearVelocityY);
        _animator.SetBool(_isGroundedHash, _isGrounded);
        _animator.SetBool(_attackNowHash, _atkNow);

        _rb.linearVelocityX = _moveDir * speed;

        CheckGround();
        
        transform.localRotation = Quaternion.Euler(0, _lastMoveDir > 0 ? 0 : 180f, 0f);

    
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
        _playerAtkTimer = 1f; // 플레이어 콤보 쿨타임 1초로 설정
        if (_atkTime) // 공격 중이 아닐때
        StartCoroutine(AtkCoroutine()); // 코루틴 실행


        if (!_atkNow) // 공격 중 일때
        {
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
    }

    IEnumerator AtkCoroutine()
    {
        _atkTime = false;
        while (_playerAtkTimer > 0) // 1초동안 입력없으면 초기화
        {
            _playerAtkTimer -= Time.deltaTime;
            yield return null;
        }
        _playerAtkCombo = 0;
        _atkTime = true;
    }
    IEnumerator AtkCoolDown()
    {
        _atkNow = true;
        _playerAtkCombo++;
        yield return new WaitForSeconds(0.3f);
        _atkNow = false;
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

        if (_currentJumpCount > 0)
        {
            _rb.linearVelocityY = 0;
            _rb.AddForceY(JumpPower, ForceMode2D.Impulse);

            _currentJumpCount -= 1;
        }
    }
}

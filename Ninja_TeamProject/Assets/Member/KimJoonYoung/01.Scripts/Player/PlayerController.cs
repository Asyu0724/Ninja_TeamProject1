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

    public float Speed
    {
        get => _agentMover._rb.linearVelocityX / _speed;
        private set {}
    }

    private int _currentJumpCount;
    private float _moveDir;
    private float _lastMoveDir = 1;
    private bool _isGrounded;

    public bool _playerHited;

    // Scripts
    private HealthSystem _healthSystem;
    private PlayerSkill _playerSkill;

    // Hash
    private int _xMoveHash = Animator.StringToHash("X_Move");
    private int _yVelocityHash = Animator.StringToHash("Y_Velocity");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _playerHitedHash = Animator.StringToHash("PlayerHited");

    private bool _SkillUse;

    /*---------------------------------------------------*/ // Initialization
    protected override void Awake()
    {
        base.Awake();
        _currentJumpCount = _jumpCount;
        _healthSystem = GetComponent<HealthSystem>();
        _playerSkill = GetComponent<PlayerSkill>();
    }

    private void Start()
    {
        UIManager.Instance.HealthUI.InitHealthUI(_healthSystem.Health);
        _healthSystem.OnDamaged += UpdateHealthUI;
    }

    /*---------------------------------------------------*/ // Physics 

    private void FixedUpdate()
    {
        if (!_SkillUse)
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
        if (!_playerHited && !_SkillUse)
        {
            if (_currentJumpCount < 1) return;

            if (_currentJumpCount > 0)
            {
                _agentMover.AddForceToAgent(_jumpPower * Vector2.up);
                _currentJumpCount--;
            }
        }
    }
    /*---------------------------------------------------*/ // Game logic

    private void Update()
    {
        _agentRenderer.SetFloatParam(_xMoveHash, Mathf.Abs(_moveDir));
        _agentRenderer.SetFloatParam(_yVelocityHash, _agentMover._rb.linearVelocityY);
        _agentRenderer.SetBoolParam(_isGroundedHash, _isGrounded);
        _agentRenderer.SetBoolParam(_playerHitedHash, _playerHited);

        _SkillUse = _playerSkill._qSkillUse;
    }

    /*---------------------------------------------------*/ // Inumerator
    public IEnumerator PlayerHited()
    {
        _playerHited = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);    
        yield return new WaitForSeconds(0.75f);
        _playerHited = false;
    }
    /*---------------------------------------------------*/ // Game method

    private void Flip()
    {
        if (0 != _lastMoveDir)
        {
            transform.rotation = Quaternion.Euler(0, _lastMoveDir > 0 ? 0 : 180f, 0f); // 플립
        }
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

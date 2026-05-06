using System.Collections;
using Member.KimJoonYoung._01.Scripts.Interface;
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
    public float Speed
    {
        get => _agentMover._rb.linearVelocityX / _speed;
        private set {}
    }
    [SerializeField] private float _speed;
    [SerializeField] private int _jumpPower;
    [SerializeField] private int _jumpCount;
    private int _currentJumpCount;
    private float _lastMoveDir = 1;
    private bool _isGrounded;
    private Vector2 _moveDir;

    public bool PlayerHit {get; private set;}

    // Scripts
    private HealthSystem _healthSystem;
    private PlayerAttackManager _playerAttackManager;

    // Hash
    private readonly int _xMoveHash = Animator.StringToHash("X_Move");
    private readonly int _yVelocityHash = Animator.StringToHash("Y_Velocity");
    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int _playerHitedHash = Animator.StringToHash("PlayerHited");

    private bool _skillUse;

    /*---------------------------------------------------*/ // Initialization
    protected override void Awake()
    {
        base.Awake();
        _currentJumpCount = _jumpCount;
        _healthSystem = GetComponent<HealthSystem>();
        _playerAttackManager = GetComponent<PlayerAttackManager>();
    }

    private void Start()
    {
        UIManager.Instance.HealthUI.InitHealthUI(_healthSystem.Health);
        _healthSystem.OnDamaged += UpdateHealthUI;
    }

    /*---------------------------------------------------*/ // Physics 

    private void FixedUpdate()
    {
        _isGrounded = _agentMover.CheckGround();
        if (!_skillUse)
        {
            _agentMover.Move(_moveDir.x * _speed);
            if (_isGrounded && _agentMover._rb.linearVelocityY <= 0)
                _currentJumpCount = _jumpCount;

            /*_agentMover._rb.linearVelocityX = _moveDir * _speed;*/

            Flip();
            _agentAttack.Flip(_lastMoveDir);
        }
    }

    /*---------------------------------------------------*/ // Input event

    private void OnMove(InputValue value)
    {
        _moveDir.x = value.Get<Vector2>().x;
        if (_moveDir.x != 0)
            _lastMoveDir = value.Get<Vector2>().x;
    }
    
    private void OnJump(InputValue value)
    {
        if (!_skillUse)
        {
            if (_currentJumpCount < 1) return;

            if (_currentJumpCount > 0)
            {
                _agentMover.Jump(_jumpPower);
                _currentJumpCount--;
            }
        }
    }
    /*---------------------------------------------------*/ // Game logic

    private void Update()
    {
        _agentRenderer.SetFloatParam(_xMoveHash, Mathf.Abs(_moveDir.x));
        _agentRenderer.SetFloatParam(_yVelocityHash, _agentMover._rb.linearVelocityY);
        _agentRenderer.SetBoolParam(_isGroundedHash, _isGrounded);
        _agentRenderer.SetBoolParam(_playerHitedHash, PlayerHit);

        _skillUse = _playerAttackManager._qSkillUse;
    }

    /*---------------------------------------------------*/ // Inumerator
    public IEnumerator PlayerHited()
    {
        PlayerHit = true;
        GameManager.Instance.timeScaleManager.OnHit();
        GameManager.Instance.bloomManager.OnHit();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);    
        yield return new WaitForSeconds(_healthSystem.InvTime);
        GameManager.Instance.timeScaleManager.OffHit();
        GameManager.Instance.bloomManager.OffHit(); 
        PlayerHit = false;
        /*TimeScaleManager.Instance.OffHit();
        BloomManager.Instance.OffHit();*/
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
        if (collision.gameObject.TryGetComponent<ICollisionAttackable> (out ICollisionAttackable attackable) && !PlayerHit)
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

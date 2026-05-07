using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossMover : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private Vector2 _moveDir;
    private Vector2 _distance;

    public TestPlayerController player;

    // 범위제한
    private float minLimit;
    private float maxLimit;
    private float offset = 1.5f;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    // 보스가 공격을 할수 있는지 체크
    private bool _isCanAttack;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset;

    // 보스가 바닥과 닿아 있는지 체크
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private Vector2 groundOffset;

    [SerializeField] private BossHealth _bossHP;
    [SerializeField] private BossSkill _bossSkill;
    [SerializeField] private BossRenderer _bossRenderer;


    private bool _isSkill => _Attack1 || _Attack2 || _bossHP._isCharge || _isJump || _Attack3;

    public float _lastAttackTime { get; private set; }
    // public bool _isGrounded { get; private set; }
    public bool _Attack1 { get; private set; }
    public bool _Attack2 { get; private set; }

    public bool _Attack3 { get; private set; }
    public bool _isJump { get; private set; }


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        minLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        maxLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;

    }

    private void FixedUpdate()
    {
        _distance = player.transform.position - transform.position;
        _moveDir.x = _distance.x > 0 ? 1f : -1f;
        
        if (Mathf.Abs(_distance.x) < 2.0f)
        {
            _moveDir.x = 0;
            if (_distance.y > 0.6f)
            {
                if (!_isSkill) _Attack3 = true;
                else if(!_Attack3) _bossRenderer.AnimSpeed(1.5f);
            }
        }
        if (Mathf.Abs(_distance.x) > 6.0f && !_isSkill)
        {
            _isJump = true; 
            // _rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if(!_isSkill) _rigid.linearVelocityX = _moveDir.x * speed;

        CheckOverlap();

        if (_isCanAttack == true && !_isSkill && _lastAttackTime <= 0)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        int random = Random.Range(0, 2);
        if (random == 0) _Attack1 = true;
        else if (random == 1) _Attack2 = true;

        _lastAttackTime = 2f;
    }

    public void SkillOff()
    {
        _Attack1 = false;
        _Attack2 = false;
        _Attack3 = false;
        _bossHP.ChargeHP(false);
        _isJump = false;
        _bossRenderer.AnimSpeed(1.0f);
    }

    public void MoveToPlayer()
    {
        transform.position = player.transform.position;
    }

    private void Update() 
    {
        _lastAttackTime -= Time.deltaTime;

        if (!_isSkill) // 회전
        {
            if (_moveDir.x > 0)
            {
                if (boxOffset.x < 0) boxOffset.x *= -1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_moveDir.x < 0)
            {
                if (boxOffset.x > 0) boxOffset.x *= -1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void CheckOverlap()
    {
        _isCanAttack = Physics2D.OverlapBox(transform.position + (Vector3)boxOffset, boxSize, 0, PlayerLayer);

        // _isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)groundOffset, groundBoxSize, 0, isGround);

        // if(_isCanAttack) Debug.Log("플레이어 확인됨");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)boxOffset, boxSize);
        // Gizmos.DrawWireCube(transform.position + (Vector3)groundOffset, groundBoxSize);
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minLimit + offset, maxLimit - offset), transform.position.y, transform.position.z);
    }
}

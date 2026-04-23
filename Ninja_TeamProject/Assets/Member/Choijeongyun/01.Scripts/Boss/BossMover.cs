using UnityEngine;
using UnityEngine.InputSystem;

public class BossMover : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private Vector2 _moveDir;
    private float _distance;

    public Transform player;

    // 범위제한
    private float minLimit;
    private float maxLimit;
    private float offset = 0.5f;

    [SerializeField] private float speed;

    private bool _isCanAttack;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset;

    [SerializeField] private LayerMask isGround;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private Vector2 groundOffset;

    [SerializeField] private BossHealth _bossHP;


    private bool _isSkill => _Attack1 || _Attack2 || _Charge || _isJump;

    public float _lastAttackTime { get; private set; }
    public bool _isGrounded { get; private set; }
    public bool _Attack1 { get; private set; }
    public bool _Attack2 { get; private set; }
    public bool _Charge { get; private set; }
    public bool _Death { get; private set; }
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
        _distance = player.position.x - transform.position.x;
        _moveDir.x = _distance > 0 ? 1f : -1f;

        if (Mathf.Abs(_distance) < 2.0f) _moveDir.x = 0;
        if (Mathf.Abs(_distance) > 5.0f && _isGrounded == true)
        {
            _isJump = true;
            // transform.position = player.transform.position;
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
        _bossHP.ChargeHP(false);
        _isJump = false;
    }

    private void Update() // 회전
    {
        _lastAttackTime -= Time.deltaTime;

        if (!_isSkill)
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

        _isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)groundOffset, groundBoxSize, 0, isGround);

        // if(_isCanAttack) Debug.Log("플레이어 확인됨");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)boxOffset, boxSize);
        Gizmos.DrawWireCube(transform.position + (Vector3)groundOffset, groundBoxSize);
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minLimit + offset, maxLimit - offset), transform.position.y, transform.position.z);
    }
}

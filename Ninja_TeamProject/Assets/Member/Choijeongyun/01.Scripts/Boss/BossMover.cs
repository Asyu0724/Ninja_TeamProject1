using System.Collections;
using System.Diagnostics;
using DG.Tweening;
using Member.Choijeongyun._01.Scripts.Func;
using Member.KimJoonYoung._01.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Sequence = DG.Tweening.Sequence;

public class BossMover : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private Vector2 _moveDir;
    private Vector2 _distance;
    private Vector2 _dashDir;

    [SerializeField] private PlayerController player;

    // 범위제한
    private float _minLimit;
    private float _maxLimit;
    private float _offset = 1.5f;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float dashPower;

    // 보스가 공격을 할수 있는지 체크
    private bool _isCanAttack;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset;

    // 보스가 바닥과 닿아 있는지 체크
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private Vector2 groundOffset;

    [SerializeField] private BossHealth bossHP;
    [SerializeField] private BossSkill bossSkill;
    [SerializeField] private BossRenderer bossRenderer;
    [SerializeField] private CJY_AudioManager bossAudio;

    private bool _isPlaySound = false;
    private bool _dashIsCoolT;
    private bool _isGrounded;
    
    private bool IsSkill => Attack1 || Attack2 || bossHP.IsCharge || IsJump || Attack3 || IsDash;

    public bool NotOtherSkill
    {
        get => Attack1 || Attack2 || IsJump || Attack3 || IsDash;
        set => NotOtherSkill = value;
    } 

    public float LastAttackTime { get; private set; }
    // public bool _isGrounded { get; private set; }
    public bool Attack1 { get; private set; }
    public bool Attack2 { get; private set; }

    public bool Attack3 { get; private set; }
    public bool IsJump { get; private set; }

    public bool IsShake { get; private set; }
    public bool IsDash { get; private set; }


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    /*private void Start()
    {
        _minLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        _maxLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;

    }*/
    
    private bool CheckGround() // 그라운드 감지
    {
        _isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)groundOffset, groundBoxSize, 0, isGround);
        return _isGrounded;
    }
    
    private IEnumerator DashEnd()
    {
        yield return new WaitForSeconds(0.25f);
        IsDash = false;
        yield return new WaitForSeconds(3f);
        _dashIsCoolT = false;
    }

    private void StartDashCo()
    {
        StartCoroutine(DashEnd());
    }

    private void FixedUpdate()
    {
        _distance = player.transform.position - transform.position;
        _moveDir.x = _distance.x > 0 ? 1f : -1f;
        
        if (Mathf.Abs(_distance.x) < 2.0f)
        {
            // _moveDir.x = 0;
            if(!IsSkill && !_dashIsCoolT && _isGrounded)
            {
                IsDash = true; 
                _dashIsCoolT = true;
                Sequence seq =  DOTween.Sequence();
                _dashDir.x = transform.position.x >= 0 ? -1f : 1f;
                bossRenderer.StartSFX();
                seq.Prepend(_rigid.DOMoveX(transform.position.x + _moveDir.x * dashPower, 0.5f).SetEase(Ease.OutQuart));
                seq.OnComplete(StartDashCo);
            }
            
            if (_distance.y < 0.2f && _isGrounded)
            {
                if (!IsSkill)
                {
                    //bossAudio.PlaySFX(5, 0.2f);
                    Attack3 = true;
                }
                // else if(!Attack3) bossRenderer.AnimSpeed(1.5f); 
            }

            /*if (!IsSkill) // 가까워 지면 점프 
            {
                IsJump = true;
                bossAudio.PlaySFX(1,0.1f);   
            }*/
        }
        if (Mathf.Abs(_distance.x) > 7.0f && !IsSkill)
        {
            IsJump = true;
            //bossAudio.PlaySFX(1,0.1f);   
            // _rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if(!IsSkill)
        {
            _rigid.linearVelocityX = _moveDir.x * speed;
            if (_moveDir.x != 0 && !_isPlaySound)
            {
                bossAudio.PlayLoop(0);
                _isPlaySound = true;
            }
            else if (_moveDir.x == 0 && _isPlaySound) 
            {
                bossAudio.StopLoop();
                _isPlaySound = false;
            }
        }
        else
        {
            bossAudio.StopLoop();
            _isPlaySound = false;
        }

        CheckOverlap();

        if (_isCanAttack == true && !IsSkill && LastAttackTime <= 0)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            //bossAudio.PlaySFX(3,0.2f);
            Attack1 = true;
        }
        else if (random == 1)
        {
            //bossAudio.PlaySFX(4,0.4f);
            Attack2 = true;
        }

        LastAttackTime = 2f;
    }

    public void SkillOff()
    {
        Attack1 = false;
        Attack2 = false;
        Attack3 = false;
        IsJump = false;
        IsShake = false;
        // bossRenderer.AnimSpeed(1.0f);
    }

    public void MoveToPlayer()
    {
        Vector2 newPos = transform.position;
        newPos.x = player.transform.position.x;
        transform.position = newPos;
        IsShake = true;
        //bossAudio.PlaySFX(2, 0.1f);
    }

    private void Update() 
    {
        CheckGround();
        LastAttackTime -= Time.deltaTime;
        if (!IsSkill) // 회전
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
        _isCanAttack = Physics2D.OverlapBox(transform.position + (Vector3)boxOffset, boxSize, 0, playerLayer);

        // _isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)groundOffset, groundBoxSize, 0, isGround);

        // if(_isCanAttack) Debug.Log("플레이어 확인됨");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)boxOffset, boxSize);
        Gizmos.DrawWireCube(transform.position + (Vector3)groundOffset, groundBoxSize);
    }
    
    public void StartBossSFX(int value)
    {
        bossAudio.PlaySFX(value, 0);
    }


    /*private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minLimit + _offset, _maxLimit - _offset), transform.position.y, transform.position.z);
    }*/
}

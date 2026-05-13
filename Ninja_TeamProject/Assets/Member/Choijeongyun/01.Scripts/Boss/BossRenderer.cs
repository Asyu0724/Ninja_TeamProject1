using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossRenderer : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;
    private SpriteRenderer _rangeRenderer;
    private SpriteRenderer _chargeRenderer;
    
    [SerializeField] private GameObject range;
    [SerializeField] private GameObject charge;
    [SerializeField] private BossMover bossMove;
    [SerializeField] private BossHealth bossHP;

    [SerializeField] private Transform player;

    private int _xMoveHash = Animator.StringToHash(name: "MoveX");
    // private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _attack1Hash = Animator.StringToHash(name: "Attack1");
    private int _attack2Hash = Animator.StringToHash(name: "Attack2");
    private int _attack3Hash = Animator.StringToHash(name: "Attack3");
    private int _chargeHash = Animator.StringToHash(name: "Charge");
    private int _deathHash = Animator.StringToHash(name: "Death");
    private int _jumpHash = Animator.StringToHash(name: "Jump");

    private float _moveX;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rangeRenderer = range.GetComponentInChildren<SpriteRenderer>();
        _chargeRenderer = charge.GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _moveX = Mathf.Abs(bossMove.GetComponent<Rigidbody2D>().linearVelocityX);
    }

    private void Update()
    {
        _animator.SetBool(_attack1Hash, bossMove.Attack1);
        _animator.SetBool(_attack2Hash, bossMove.Attack2);
        _animator.SetBool(_attack3Hash, bossMove.Attack3);
        _animator.SetFloat(_xMoveHash, _moveX);
        _animator.SetBool(_deathHash, bossHP.IsDeath);
        _animator.SetBool(_chargeHash, bossHP.IsCharge);
        _animator.SetBool(_jumpHash, bossMove.IsJump);
        // _animator.SetBool(_isGroundedHash, bossMove._isGrounded);
 
    }

    private void LateUpdate()
    {
        if (bossMove.IsJump)
        {
            Vector2 playerPos = new Vector2(player.transform.position.x, -5.5f);
            range.transform.position = playerPos;
        }
    }

    public void AnimationFinished()
    {
        bossMove.SkillOff();
    }

    public void JumpFinished()
    {
        bossMove.MoveToPlayer();
    }
    
    public void RangeStart()
    {
        _rangeRenderer.enabled = true;
    }

    public void RangeEnd()
    {
        _rangeRenderer.enabled = false;
    }

    public void AnimSpeed(float value)
    {
        _animator.speed = value;
    }

    public void BossDie()
    {
        gameObject.SetActive(false); // 임시방편
    }

    public IEnumerator JumpDel()
    {
        this._animator.speed = 0;
        this._renderer.enabled = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Boss"), true);
        yield return new WaitForSeconds(1f);
        this._animator.speed = 1;
        this._renderer.enabled = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Boss"), false);
    }

    public void ChargeStart()
    {
        _chargeRenderer.enabled = true;
    }

    public void ChargeEnd()
    {
        _chargeRenderer.enabled = false;
        _animator.speed = 1;
    }

    public void ChargeAnimeEnd()
    {
        _animator.speed = 0;
    }
    
    private IEnumerator Attacked()
    {
        _renderer.color = Color.red;
        Color color = _renderer.color;
        
        color.a = 0.7f;
        _renderer.color = color;
        
        yield return new WaitForSeconds(0.1f);
        
        color.a = 1f;
        _renderer.color = color;
        
        yield return new WaitForSeconds(0.1f);
        
        color.a = 0.7f;
        _renderer.color = color;
        
        yield return new WaitForSeconds(0.1f);
        
        color.a = 1f;
        _renderer.color = color;
        _renderer.color = Color.white;
    }
}

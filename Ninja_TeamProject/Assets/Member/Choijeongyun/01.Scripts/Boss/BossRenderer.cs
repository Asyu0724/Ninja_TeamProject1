using Unity.VisualScripting;
using UnityEngine;

public class BossRenderer : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private BossMover bossMove;
    [SerializeField] private BossHealth _bossHP;

    private int _xMoveHash = Animator.StringToHash(name: "MoveX");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _isjumpHash = Animator.StringToHash(name: "Jump");
    private int _attack1Hash = Animator.StringToHash(name: "Attack1");
    private int _attack2Hash = Animator.StringToHash(name: "Attack2");
    private int _chargeHash = Animator.StringToHash(name: "Charge");
    private int _deathHash = Animator.StringToHash(name: "Death");

    private float _moveX;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        _moveX = Mathf.Abs(bossMove.GetComponent<Rigidbody2D>().linearVelocityX);
    }

    private void Update()
    {
        _animator.SetBool(_attack1Hash, bossMove._Attack1);
        _animator.SetBool(_attack2Hash, bossMove._Attack2);
        _animator.SetFloat(_xMoveHash, _moveX);
        _animator.SetBool(_deathHash, _bossHP._isDeath);
        _animator.SetBool(_chargeHash, _bossHP._isCharge);
        _animator.SetBool(_isGroundedHash, bossMove._isGrounded);
    }

    public void AnimationFinished()
    {
        bossMove.SkillOff();
    }

    public void JumpFinished()
    {
        
    }
}

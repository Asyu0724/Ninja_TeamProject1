using Unity.VisualScripting;
using UnityEngine;

public class BossRenderer : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private BossMover bossMove; 

    private int _xMoveHash = Animator.StringToHash(name: "MoveX");
    private int _yMoveHase = Animator.StringToHash(name: "MoveY");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private int _Attack1Hash = Animator.StringToHash(name: "Attack1");
    private int _Attack2Hash = Animator.StringToHash(name: "Attack2");
    private int _ChargeHash = Animator.StringToHash(name: "Charge");
    private int _DeathHash = Animator.StringToHash(name: "Death");

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
        _animator.SetBool(_Attack1Hash, bossMove._Attack1);
        _animator.SetBool(_Attack2Hash, bossMove._Attack2);
        _animator.SetFloat(_xMoveHash, _moveX);
    }

    public void AnimationFinished()
    {
        bossMove.SkillOff();
    }
    

}

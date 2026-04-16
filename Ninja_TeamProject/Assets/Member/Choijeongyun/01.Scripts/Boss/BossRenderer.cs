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

    public bool _isGrounded { get; private set; }
    public bool _Attack1 { get; private set; }
    public bool _Attack2 { get; private set; }
    public bool _Charge { get; private set; }
    public bool _Death { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_Attack1Hash, bossMove._Attack1);
        _animator.SetBool(_Attack2Hash, bossMove._Attack2);
    }

    public void AnimationFinished()
    {
        bossMove.SkillOff();
    }
    

}

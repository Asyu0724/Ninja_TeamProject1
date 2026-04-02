using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float JumpPower;
    [SerializeField] private int _jumpCount;

    private Animator _animator;

    private float _moveDir;
    private Rigidbody2D _rb;

    private int _XMoveHash = Animator.StringToHash("XMove");

    private int _yVelocityHash = Animator.StringToHash("Y_Velocity");

    private int _isGroundedHash = Animator.StringToHash("IsGrounded");

    private bool _isGrounded;

    private int _currentJumpCount;

    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 offset;
    [SerializeField] private LayerMask whatIsGround;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _currentJumpCount = _jumpCount;
    }
    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>().x;
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
        /*/if (_currentJumpCount > 0)
         {

             _isGrounded = true;
             Debug.Log(_currentJumpCount);
             --_currentJumpCount;
             return;
         }
         else
         {
             _isGrounded = false;
         }*/

    }
    private void FixedUpdate()
    {
        _animator.SetFloat(_XMoveHash, Mathf.Abs(_moveDir));
        _animator.SetFloat(_yVelocityHash, _rb.linearVelocityY);
        _animator.SetBool(_isGroundedHash, _isGrounded);

        _rb.linearVelocityX = _moveDir * speed;

        CheckGround();
    }
    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)offset,
            boxSize, 0, whatIsGround);
        if (_isGrounded)
        {
            _currentJumpCount = _jumpCount;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        _isGrounded = true;
    //        _animator.SetBool(_isGroundedHash, true);
    //        _currentJumpCount = _jumpCount;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        _isGrounded = false;
    //    }
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, boxSize);
    }
}

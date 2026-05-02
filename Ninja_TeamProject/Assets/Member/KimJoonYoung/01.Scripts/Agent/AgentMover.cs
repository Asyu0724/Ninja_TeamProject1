using System.Xml.Serialization;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    public Rigidbody2D _rb { get; private set; }
    public bool isGrounded { get; private set; }
    // 오버랩
    [Header("OverLab")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 offset;
    [SerializeField] private LayerMask whatIsGround;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    public void AddForceToAgent(Vector2 force) // 점프
    {
        _rb.linearVelocityY = 0;
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public bool CheckGround() // 그라운드 감지
    {
        isGrounded = Physics2D.OverlapBox(transform.position + (Vector3)offset, boxSize, 0, whatIsGround);
        return isGrounded;
    }

    public void Move(float value)
    {
        _rb.linearVelocityX = value;
    }

    public void Jump(float value)
    {
        AddForceToAgent(value * Vector2.up);
    }

    private void OnDrawGizmos() // 기즈모
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset,boxSize);
    }
}

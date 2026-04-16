using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    private float _moveDir;
    private Rigidbody2D _rigid;
    
    // 범위제한
    private float minLimit;
    private float maxLimit;
    private float offset = 0.5f;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        minLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        maxLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
    }

    private void OnMove(InputValue value)  // a, d 받기
    {
        _moveDir = value.Get<Vector2>().x; // OnMove에서 받은 x 값을 _moveDir에 복사
    }

    private void OnJump()
    {
        _rigid.AddForceY(_jumpPower, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocityX = _moveDir * _speed;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minLimit + offset, maxLimit - offset), transform.position.y, transform.position.z);
    }
}


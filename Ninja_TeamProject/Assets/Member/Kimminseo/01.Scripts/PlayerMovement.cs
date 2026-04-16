using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    private Animator animator;
    private Vector2 minLimit;
    private Vector2 maxLimit;
    private float offset = 0.5f;
    private Vector2 currentPos;


    private Rigidbody2D rigid;

    private Vector2 moveDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        minLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void FixedUpdate()//일정 주기마다 반복
    {
        rigid.linearVelocity = moveDir * speed;

    }
    private void LateUpdate()
    {
        currentPos.x = Mathf.Clamp(transform.position.x, minLimit.x + offset, maxLimit.x - offset);
        currentPos.y = Mathf.Clamp(transform.position.y, minLimit.y + offset, maxLimit.y - offset);
        transform.position = currentPos;
    }


    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }
}

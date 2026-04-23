using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rigid;
    private Vector2 moveDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.linearVelocityX = moveDir.x * speed;
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();   
    }
}

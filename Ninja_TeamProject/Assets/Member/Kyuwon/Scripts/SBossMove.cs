using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class SBossMove : MonoBehaviour
{
    private float speed = 3.0f;
    [SerializeField]private Rigidbody2D rigid;
    [SerializeField]private Transform playerTRM;
    [SerializeField]private Vector2 moveDir;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerTRM = GameObject.Find("Square").transform;
    }

    private void FixedUpdate()
    {
        if (playerTRM != null)
        {
            moveDir = (playerTRM.position - transform.position).normalized;
            rigid.linearVelocityX = moveDir.x * speed;
            if (moveDir.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (moveDir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void Update()
    {
        
    }
}

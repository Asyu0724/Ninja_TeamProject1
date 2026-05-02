using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SBossMove : MonoBehaviour
{
    private float speed = 3.0f;
    [SerializeField]private Rigidbody2D rigid;
    [SerializeField]private Transform playerTRM;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Animator animator;
    private bool isAttacking = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerTRM = GameObject.Find("Square").transform;
        animator = GetComponentInChildren<Animator>();
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

            int BossMove = Mathf.Abs(moveDir.x) >= 0.1f ? 1 : 0;
            animator.SetFloat("MoveX", BossMove);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("UMM") && !isAttacking)
        {
            Debug.Log("sj");
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        animator.SetTrigger("Slash");

        yield return new WaitForSeconds(1.0f);

        isAttacking = false;

        rigid.linearVelocityX = 3.0f;
    }
}

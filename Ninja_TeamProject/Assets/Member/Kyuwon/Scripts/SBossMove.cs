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
    [SerializeField] private float chargingCool = 9.0f;
    [SerializeField] private float TelCool = 18.0f;
    private bool charcoolTime = false;
    private bool TelcoolTime = false;

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
            float distance = Vector2.Distance(transform.position, playerTRM.position);
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

            if (distance < 10.0f && charcoolTime == false)
            {
                Debug.Log("이얏");
                charcoolTime = true;
            }
            if (charcoolTime == true)
            {
                chargingCool -= Time.deltaTime;

                if (chargingCool <= 0f)
                {
                    charcoolTime = false;
                    chargingCool = 9.0f;
                }
            }

            if (distance > 17.0f && distance < 20.0f && TelcoolTime == false)
            {
                Debug.Log("호잇");
                TelcoolTime = true;
            }
            if (TelcoolTime == true)
            {
                TelCool -= Time.deltaTime;

                if (TelCool <= 0f)
                {
                    TelcoolTime = false;
                    TelCool = 18.0f;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("UMM"))
        {
            Debug.Log("sj");
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        int randomValue = Random.Range(0, 5);

        if (randomValue < 1)
        {
            Debug.Log("어류겐");
        }
        else
        {
            Debug.Log("휭");
            animator.SetTrigger("Slash");
        }

        yield return new WaitForSeconds(1.5f);

        rigid.linearVelocityX = 3.0f;
    }
}

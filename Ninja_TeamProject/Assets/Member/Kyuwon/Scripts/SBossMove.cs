using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SBossMove : MonoBehaviour
{
    private float speed = 3.0f;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Transform playerTRM;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Animator animator;
    [SerializeField] private float chargingCool = 9.0f;
    [SerializeField] private float TelCool = 18.0f;
    private bool charcoolTime = false;
    private bool TelcoolTime = false;
    public float faceCool = 0.5f;


    private float _lastChagingTime = 0;
    private float distance;
    private bool CanCharing => Time.time >= _lastChagingTime + chargingCool;
    
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
            Move();
            RotateBoss();

            int BossMove = Mathf.Abs(moveDir.x) >= 0.1f ? 1 : 0;
            animator.SetFloat("MoveX", BossMove);

            if (distance < 10.0f && CanCharing == false)
            {
                //StartCoroutine(ChargeRoutine());
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

    private void Update()
    {
        //bool detectPlayer = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
    }

    private void Move()
    {
        distance = Vector2.Distance(transform.position, playerTRM.position);
        moveDir = (playerTRM.position - transform.position).normalized;
        rigid.linearVelocityX = moveDir.x * speed;
    }

    private void RotateBoss()
    {
        if (moveDir.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //speed = 0;
            faceCool -= Time.deltaTime;
            if (faceCool <= 0)
            {
                speed = 3.0f;
                faceCool = 0.5f;
            }
        }
        else if (moveDir.x <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //speed = 0;
            faceCool -= Time.deltaTime;
            if (faceCool <= 0)
            {
                speed = 3.0f;
                faceCool = 0.5f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UMM"))
        {
            //StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        speed = 0;
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

        yield return new WaitForSeconds(1.0f);
        speed = 3.0f;
    }

    private IEnumerator ChargeRoutine()
    {
        speed = 0;
        Debug.Log("이얏");
        animator.SetTrigger("Charging");
        charcoolTime = true;

        if (CanCharing == true)
        {
            charcoolTime = false;
            _lastChagingTime = Time.time;
        }

        yield return new WaitForSeconds(1.0f);
        
        speed = 3.0f;
    }
}
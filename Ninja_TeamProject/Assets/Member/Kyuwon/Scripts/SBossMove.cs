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
            Rotation();

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
        moveDir = (playerTRM.position - transform.position).normalized;
        rigid.linearVelocityX = moveDir.x * speed;
    }

    private void Rotation()
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
}
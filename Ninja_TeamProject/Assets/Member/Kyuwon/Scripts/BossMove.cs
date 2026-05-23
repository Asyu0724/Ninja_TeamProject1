using System;
using System.Collections;
using Member.Kyuwon.SBossSO;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class BossMove : MonoBehaviour
{
    public SBossData bossData;
    [field: SerializeField] public SBoss _sBoss;
    private Animator _animator;
    private Rigidbody2D rigid;
    private Vector2 MoveDir; 
    private Transform playerTRM;
    public float MoveSpeed;
    public static BossMove instance;
    [SerializeField] private LayerMask whatIsWall;
    
    private void Awake()
    {
        playerTRM = GameObject.Find("Player").transform;
        _animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        MoveSpeed = bossData.speed;
        instance = this;
    }
    
    private void Update()
    {
        float PlayerDistance = Vector2.Distance(transform.position, playerTRM.position);
        float StopDistance = 2f;
        
        if (PlayerDistance > StopDistance)
        {
            float tempDir = (playerTRM.position.x - transform.position.x);
            tempDir = Mathf.Sign(tempDir);
            MoveDir = new Vector2(tempDir, 0f);
            
            rigid.linearVelocityX = MoveSpeed * MoveDir.x;
            
            //_animator.SetFloat("MoveX", MoveDir.x);
            _animator.SetFloat("MoveX", Mathf.Abs(MoveDir.x));
            Flip();
        }
        else
        {
            rigid.linearVelocityX = 0f;
            _animator.SetFloat("MoveX", 0f);
        }
    }
    
    private void Flip()
    {
        if (BossCharge.instance._dontFlip) return;

        if (MoveDir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (MoveDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void ChargeChange()
    {
        MoveSpeed = 0f;

        Vector2 bossLook = transform.right; 
        float ChargePoint = bossData.ChargeRange.x; 
        
        Vector2 startPos = (Vector2)transform.position + (bossLook * 0.5f);
        
        RaycastHit2D hit = Physics2D.Raycast(startPos, bossLook, ChargePoint, whatIsWall);
        Vector2 ChargeEnd;

        if (hit.collider != null)
        {
            ChargeEnd = hit.point - (bossLook * 0.5f);
        }
        else
        {
            ChargeEnd = (Vector2)transform.position + (bossLook * ChargePoint);
        }
        transform.position = ChargeEnd;
        
        Debug.Log("위치 이동 완료");
    }
}

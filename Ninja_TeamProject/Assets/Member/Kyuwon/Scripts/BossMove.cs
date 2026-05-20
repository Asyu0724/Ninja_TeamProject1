using System;
using System.Collections;
using Member.Kyuwon.SBossSO;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class BossMove : MonoBehaviour
{
    public SBossData bossData;

    private Animator _animator;
    private Rigidbody2D rigid;
    private Vector2 MoveDir; 
    private Transform playerTRM;
    public float MoveSpeed;
    public static BossMove instance;
    
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
        BossMove.instance.MoveSpeed = 0f;
        Debug.Log("위치 이동");
    }
}

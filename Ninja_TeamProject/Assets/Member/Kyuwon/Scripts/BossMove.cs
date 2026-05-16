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
    private float MoveSpeed;
    
    private void Awake()
    {
        playerTRM = GameObject.Find("exSquare").transform;
        _animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        MoveSpeed = bossData.speed;
    }
    
    private void Update()
    {
        float tempDir = (playerTRM.position.x - transform.position.x);
        tempDir = Mathf.Sign(tempDir);
        MoveDir = new Vector2(tempDir, 0f);
        rigid.linearVelocityX = MoveSpeed * MoveDir.x;
        
        _animator.SetFloat("MoveX", MoveDir.x);
        _animator.SetFloat("MoveX", Mathf.Abs(MoveDir.x));
        Flip();
    }
    
    private void Flip()
    {
        if (MoveDir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(FlipCool());
        }
        else if (MoveDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            StartCoroutine(FlipCool());
        }
    }

    IEnumerator FlipCool()
    {
        MoveSpeed = 0f;
        yield return new WaitForSeconds(0.5f);
        MoveSpeed = 3.0f;
    }
}

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
    
    private void Awake()
    {
        playerTRM = GameObject.Find("exSquare").transform;
        _animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        float tempDir = (playerTRM.position.x - transform.position.x);
        tempDir = Mathf.Sign(tempDir);
        MoveDir = new Vector2(tempDir, 0f);
        rigid.linearVelocityX = bossData.speed * MoveDir.x;
        
        _animator.SetFloat("MoveX", MoveDir.x);
        _animator.SetFloat("MoveX", Mathf.Abs(MoveDir.x));
        Flip();
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
}

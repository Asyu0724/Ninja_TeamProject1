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
        MoveDir = (playerTRM.position - transform.position).normalized;
        rigid.linearVelocityX = bossData.speed * MoveDir.x;
    }
}

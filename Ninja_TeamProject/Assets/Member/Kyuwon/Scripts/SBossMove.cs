using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class SBossMove : MonoBehaviour
{
    [SerializeField]private float speed = 7f;
    [SerializeField]private Rigidbody2D rigid;
    [SerializeField]private Transform playerTRM;
    [SerializeField]private Vector2 moveDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerTRM = GameObject.Find("UMM").transform;
        moveDir = playerTRM.position - transform.position.normalized;
        rigid.linearVelocityX = moveDir.x * speed;
    }
}

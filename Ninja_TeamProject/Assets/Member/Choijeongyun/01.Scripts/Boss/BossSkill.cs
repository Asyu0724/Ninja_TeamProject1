using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    // 스크립트 가져오기
    [SerializeField] private BossMover bossMove;
    [SerializeField] protected TestPlayerController player;

    // 보스 범위 공격 
    [SerializeField] private Transform areaStart;
    [SerializeField] private Transform areaEnd;


    // 보스 범위 공격 (오버랩 박스)
    /*[SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset; */
    [SerializeField] private Transform dirStart;
    [SerializeField] private Transform dirEnd;


    private bool isAttackFin;

    public bool isKnockBack { get; private set; } 

    private Vector2 knockBackDir; // 넉백 방향
    [SerializeField] private float speed = 5f; // 넉백 세기

    private void AttackStart()
    {
        isAttackFin = false;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (!isAttackFin)
        {
            CheckCollision();
            yield return null;
        }
    }

    private IEnumerator KnockBackRoutine()
    {
        isKnockBack = true;
        yield return new WaitForSeconds(0.5f);
        isKnockBack = false;
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(this.areaStart.position, this.areaEnd.position);
        /*Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)boxOffset, boxSize, 0, playerLayer);*/

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                player = colliders[i].GetComponent<TestPlayerController>(); // 컨트롤러 가져오기

                if (player != null && player.GetComponent<Rigidbody2D>() != null) // 플레이어가 감지되면
                {

                    knockBackDir = this.dirStart.position - this.dirEnd.position; // 넉백

                    // 플레이어 리지드바디 이동
                    StartCoroutine(KnockBackRoutine());
                    player.GetComponent<Rigidbody2D>().AddForce(-knockBackDir.normalized * speed, ForceMode2D.Impulse);

                    if (!isAttackFin)
                    {
                        player.ChangeHealth(2);
                        if(bossMove._isJump) player.ChangeHealth(3);
                        isAttackFin = true;
                    }
                }

            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)boxOffset, boxSize);
    }*/

    private void AttackFin()
    {
        isAttackFin = true;
    }
}

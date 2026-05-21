using System.Collections;
using Member.KimJoonYoung._01.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

public class BossSkill : MonoBehaviour
{
    // 스크립트 가져오기
    [SerializeField] private BossMover bossMove;
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthSystem healthSystem;

    // 보스 범위 공격 
    [SerializeField] private Transform areaStart;
    [SerializeField] private Transform areaEnd;

    [SerializeField] private Transform gasParticle; 

    // 보스 범위 공격 (오버랩 박스)
    /*[SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset; */
    [SerializeField] private Transform dirStart;
    [SerializeField] private Transform dirEnd;

    private bool _isAttackFin;

    public bool IsKnockBack { get; private set; } 
    public UnityEvent onAtk3;
    

    private Vector2 _knockBackDir; // 넉백 방향
    [SerializeField] private float speed = 5f; // 넉백 세기

    private void AttackStart()
    {
        _isAttackFin = false;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (!_isAttackFin)
        {
            CheckCollision();
            yield return null;
        }
    }

    private IEnumerator KnockBackRoutine()
    {
        IsKnockBack = true;
        yield return new WaitForSeconds(0.5f);
        IsKnockBack = false;
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(this.areaStart.position, this.areaEnd.position);
        /*Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)boxOffset, boxSize, 0, playerLayer);*/

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                player = colliders[i].GetComponent<PlayerController>(); // 컨트롤러 가져오기

                if (player != null && player.GetComponent<Rigidbody2D>() != null) // 플레이어가 감지되면
                {

                    _knockBackDir = this.dirStart.position - this.dirEnd.position; // 넉백

                    // 플레이어 리지드바디 이동
                    StartCoroutine(KnockBackRoutine());
                    player.GetComponent<Rigidbody2D>().AddForce(-_knockBackDir.normalized * speed, ForceMode2D.Impulse);

                    if (!_isAttackFin)
                    {
                        healthSystem.GetDamage(2, this.gameObject);
                        if(bossMove.IsJump) healthSystem.GetDamage(3, this.gameObject);
                        
                        // player.ChangeHealth(2);
                        // if(bossMove.IsJump) player.ChangeHealth(3);
                        _isAttackFin = true;
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
        _isAttackFin = true;
    }
    
    public void Atk3Event()
    {
        gasParticle.position = transform.parent.position;
        onAtk3?.Invoke();
    }
}

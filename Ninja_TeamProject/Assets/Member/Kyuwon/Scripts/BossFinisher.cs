using UnityEngine;
using System.Collections;
using Member.Kyuwon.SBossSO;

public class BossFinisher : MonoBehaviour
{
    public SBossData bossData;

    private int Skill;
    private Animator _animator;
    [SerializeField] private LayerMask whatIsPlayer;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    public void Finisher()
    {
        Collider2D isHit = Physics2D.OverlapBox(transform.position, bossData.FinisherRange, 0,whatIsPlayer);

        if (isHit != null)
        {
            float timeStamp = Time.time;
            Skill = Random.Range(0, 5);

            if (Skill < 3)
            {
                _animator.SetTrigger("Finisher");
                StartCoroutine(CanAttack());
            }
            else
            {
                _animator.SetTrigger("SFinisher");
                StartCoroutine(CanAttack());
            }
        }
    }
    
    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(1.0f);
        SBoss sboss = GetComponent<SBoss>();
        sboss.isAttacking = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, bossData.FinisherRange);
    }
}

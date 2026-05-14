using System;
using System.Collections;
using UnityEngine;
using Member.Kyuwon.SBossSO;

public class BossCharge : MonoBehaviour
{
    public SBossData bossData;
    
    private Animator _animator;
    [SerializeField] private LayerMask whatIsPlayer;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Charge()
    {
        Collider2D isHit = Physics2D.OverlapBox(transform.position, bossData.ChargeRange, 0,whatIsPlayer);

        if (isHit != null)
        {
            float timeStamp = Time.time;
            _animator.SetTrigger("Charger");
            StartCoroutine(CanAttack());
        }
    }

    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(1.0f);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cornflowerBlue;
        Gizmos.DrawWireCube(transform.position, bossData.ChargeRange);
    }
}

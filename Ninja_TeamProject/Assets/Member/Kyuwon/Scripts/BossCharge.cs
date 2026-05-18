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
        float offsetDistance = bossData.ChargeRange.x * 0.5f;
        Vector2 ChargePosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);
        
        Collider2D isHit = Physics2D.OverlapBox(ChargePosition, bossData.ChargeRange, 0,whatIsPlayer);

        if (isHit != null)
        {
            float timeStamp = Time.time;
            _animator.SetTrigger("Charger");
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cornflowerBlue;
        
        float offsetDistance = bossData.ChargeRange.x * 0.5f;
        Vector2 ChargePosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);

        Gizmos.DrawWireCube(ChargePosition, bossData.ChargeRange);
    }
}

using System.Collections;
using Member.KimJoonYoung._01.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

public class Min_BossSkill : MonoBehaviour
{
    [SerializeField] private Min_BossMover bossMover;
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthSystem healthSystem;
    
    [SerializeField] private Transform areaStart;
    [SerializeField] private Transform areaEnd;
    
    [SerializeField] private Transform areaStart2;
    [SerializeField] private Transform areaEnd2;
    
    private bool _isAttackFin;

    private void DamageStart()
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
    
    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(this.areaStart.position, this.areaEnd.position);
        Collider2D[] colliders2 = Physics2D.OverlapAreaAll(this.areaStart2.position, this.areaEnd2.position);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                player = colliders[i].GetComponent<PlayerController>(); 

                if (player != null && player.GetComponent<Rigidbody2D>() != null) 
                {
                    if (!_isAttackFin)
                    {
                        healthSystem.GetDamage(1, this.gameObject);
                        // if(bossMover.스킬이름) healthSystem.GetDamage(3, this.gameObject);
                        
                        _isAttackFin = true;
                    }
                }

            }
        }
        
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].CompareTag("Player"))
            {
                player = colliders2[i].GetComponent<PlayerController>(); 

                if (player != null && player.GetComponent<Rigidbody2D>() != null) 
                {
                    if (!_isAttackFin)
                    {
                        healthSystem.GetDamage(1, this.gameObject);
                        // if(bossMover.스킬이름) healthSystem.GetDamage(3, this.gameObject);
                        
                        _isAttackFin = true;
                    }
                }

            }
        }
    }
    
    private void DamageFin()
    {
        _isAttackFin = true;
    }
}

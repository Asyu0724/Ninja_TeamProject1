using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Member.Kyuwon.SBossSO;


public class BossCharge : MonoBehaviour
{
    public SBossData bossData;
    [SerializeField] private bool isFacingRight;
    [SerializeField] public List<ParticleGroup> particles;
    private Animator _animator;
    [SerializeField] private LayerMask whatIsPlayer;

    void FixedUpdate()
    {
        isFacingRight = transform.rotation.y == 0 ? true : false;
    }
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
            
            foreach (var main in particles[0].particles)
            {
                var particle = main.main;
                particle.startRotationY = isFacingRight ? 0f : 180f * Mathf.Deg2Rad;
                main?.Play();
            }
            _animator.SetTrigger("Charger");
            
            BossMove.instance.MoveSpeed = 0f;
            _animator.SetFloat("MoveX", 0f);
        }
    }

    public void ChargeOverLap()
    {
        float offsetDistance = bossData.ChargeRange.x * 0.5f;
        Vector2 ChargePosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);
        
        Collider2D Hit = Physics2D.OverlapBox(ChargePosition, bossData.ChargeRange, 0,whatIsPlayer);
        
        Hit?.GetComponent<IDamageable>()?.GetDamage(1, gameObject);
        
        Debug.Log("Charge");
    }
}

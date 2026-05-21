using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Member.Kyuwon.SBossSO;

public class BossSlash : MonoBehaviour
{
    public SBossData bossData;
    private Animator _animator;
    [SerializeField] public List<ParticleGroup> particles;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        isFacingRight = transform.rotation.y == 0 ? true : false;
    }

    public void Slash()
    {
        float timeStamp = Time.time;

        BossCharge.instance._dontFlip = true;

        foreach (var main in particles[0].particles)
        {
            var particle = main.main;
            particle.startRotationY = isFacingRight ? 0f : 180f * Mathf.Deg2Rad;
            main?.Play();
        }

        StartCoroutine(SlashTrigger());
        
        BossMove.instance.MoveSpeed = 0f;
        _animator.SetFloat("MoveX", 0f);
    }
    
    public void SlashOverLap()
    {
        Debug.Log("DamagedSlash");
        float offsetDistance = bossData.NormalRange.x * 0.5f;
        Vector2 SlashPosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);
        
        Collider2D Hit = Physics2D.OverlapBox(SlashPosition, bossData.NormalRange, 0,whatIsPlayer);
        
        Hit?.GetComponent<IDamageable>()?.GetDamage(1, gameObject);
    }
    
    private IEnumerator SlashTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Slash");
        yield return new WaitForSeconds(1f);
        BossCharge.instance._dontFlip = false;
    }
}

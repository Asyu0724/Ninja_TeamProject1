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
    public static BossCharge instance;
    private Animator _animator;
    public bool _dontFlip;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        _dontFlip = false;
    }

    void FixedUpdate()
    {
        isFacingRight = transform.rotation.y == 0 ? true : false;
    }
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        instance = this;
    }

    public void Charge()
    {
        float timeStamp = Time.time;
        
        _dontFlip = true;

        foreach (var main in particles[0].particles)
        {
            var particle = main.main;
            particle.startRotationY = isFacingRight ? 0f : 180f * Mathf.Deg2Rad;
            main?.Play();
        }

        StartCoroutine(ChargeTrigger());

        BossMove.instance.MoveSpeed = 0f;

        _animator.SetFloat("MoveX", 0f);
        
    }

    public void ChargeOverLap()
    {
        float offsetDistance = bossData.ChargeRange.x * 0.5f;
        Vector2 ChargePosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);
        
        Collider2D Hit = Physics2D.OverlapBox(ChargePosition, bossData.ChargeRange, 0,whatIsPlayer);
        
        Hit?.GetComponent<IDamageable>()?.GetDamage(1, gameObject);
        
        Debug.Log("Charge");
    }

    private IEnumerator ChargeTrigger()
    {
        yield return new WaitForSeconds(0.6f);
        _animator.SetTrigger("Charger");
        yield return new WaitForSeconds(1f);
        _dontFlip = false;
        BossMove.instance.MoveSpeed = 3f;
    }
}

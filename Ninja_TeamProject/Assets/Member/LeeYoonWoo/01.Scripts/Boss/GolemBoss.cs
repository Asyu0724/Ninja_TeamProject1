using System.Collections;
using System.Collections.Generic;
using Member.KimJoonYoung._01.Scripts.Player;
using UnityEngine;

[System.Serializable]
public class ParticleGroup
{
    public ParticleSystem[] particles;
}

public class GolemBoss : Boss
{
    public float patternDelay = 5f;
    public float closeAttackRange = 5.0f;
    private bool isDead;

    [SerializeField] private Transform playerPos;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraShake cs;
    [SerializeField] public List<ParticleGroup> particles;
    [SerializeField] private float timeOffset;
    private bool isAttacking = false;

    Rigidbody2D _rb;

    //----------------------
    int bigCloudHash = Animator.StringToHash("BigCloud");
    int smallCloudHash = Animator.StringToHash("SmallCloud");
    int shockWaveHash = Animator.StringToHash("ShockWave");
    int spinAttackHash = Animator.StringToHash("SpinAttack");
    int spinAttackEndHash = Animator.StringToHash("SpinAttackEnd");
    int turnLeftHash = Animator.StringToHash("TurnLeft");
    int noDamageSpinAttackHash = Animator.StringToHash("NoDamageSpinAttack");
    int dieHash = Animator.StringToHash("Die");
    //----------------------


    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(BossThinkRoutine());
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            _rb.linearVelocityX = 0;
        }
    }

    protected override void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("GolemBoss Die 실행됨");

        StopAllCoroutines();

        _rb.linearVelocity = Vector2.zero;
        isAttacking = false;

        anim.ResetTrigger(turnLeftHash);

        anim.SetBool(bigCloudHash, false);
        anim.SetBool(smallCloudHash, false);
        anim.SetBool(shockWaveHash, false);
        anim.SetBool(spinAttackHash, false);
        anim.SetBool(spinAttackEndHash, false);
        anim.SetBool(noDamageSpinAttackHash, false);

        anim.SetBool(dieHash, true);
        anim.Play("Death", 0, 0f);
    }

    public void DieEvent()
    {
        //---------------------------------------------------------------------------------------------iujnwefliajerfniawuefhapiweufawoiefnawlefiwhn
    }
    
    public void AttackEnd()
    {
        anim.SetBool(spinAttackEndHash, false);
        anim.SetBool(smallCloudHash, false);
        anim.SetBool(bigCloudHash, false);
        anim.SetBool(shockWaveHash, false);
        isAttacking = false;
    }

    private IEnumerator BossThinkRoutine()
    {
        while (currentHealth > 0)
        {
            if (!isAttacking)
            {
                yield return new WaitForSeconds(1f);
                isAttacking = true;
                StartCoroutine(ChooseNextPattern());
            }

            yield return null;
        }
    }

    private IEnumerator ChooseNextPattern()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);

        if (distanceToPlayer <= closeAttackRange)
        {
            int randomPattern = UnityEngine.Random.Range(0, 4);
            if (randomPattern == 0) yield return Pattern1_BigCloud();
            if (randomPattern == 1) yield return Pattern2_SmallCloud();
            if (randomPattern == 2) yield return Pattern3_ShockWave();
            if (randomPattern == 3) yield return Pattern4_SpinAttack();
        }

        else
        {
            int randomPattern = UnityEngine.Random.Range(2, 4);
            if (randomPattern == 2) yield return Pattern3_ShockWave();
            if (randomPattern == 3) yield return Pattern4_SpinAttack();
        }
    }
    private bool isFacingRight = true;

    private IEnumerator TurnAround()
    {
        float dir = playerPos.position.x - transform.position.x;

        if (dir > 0 && !isFacingRight)
        {
            anim.SetTrigger(turnLeftHash);
            yield return new WaitForSeconds(1.82489f);

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingRight = true;
        }
        else if (dir < 0 && isFacingRight)
        {
            anim.SetTrigger(turnLeftHash);
            yield return new WaitForSeconds(1.82489f);

            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingRight = false;
        }
    }
    /*public void TurnRight()
    {
        transform.localScale = Vector3.one;
    }
    public void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }*/
    private IEnumerator Pattern1_BigCloud()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 1");
        anim.SetBool(bigCloudHash, true);
        foreach (var main in particles[2].particles)
        {
            main?.Play();
        }
        yield return new WaitForSeconds(2f/timeOffset);
        yield return new WaitForSeconds(3f/timeOffset);
    }
    private IEnumerator Pattern2_SmallCloud()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 2");
        anim.SetBool(smallCloudHash, true);
        foreach (var main in particles[2].particles)
        {
            main?.Play();
        }
        yield return new WaitForSeconds(2f/timeOffset);
    }
    private IEnumerator Pattern3_ShockWave()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 3");
        foreach (var main in particles[1].particles)
        {
            main?.Play();
        }
        anim.SetBool(shockWaveHash, true);
        yield return new WaitForSeconds(3f/timeOffset);

    }
    private IEnumerator Pattern4_SpinAttack()
    {
        float dir = (playerPos.position - transform.position).normalized.x;
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 4 시작");
        anim.SetBool(noDamageSpinAttackHash, true);
        yield return new WaitForSeconds(1.9f);
        foreach (var main in particles[0].particles)
        {
            var particle = main.main;
            particle.startRotationY = isFacingRight ? 0f : 180f * Mathf.Deg2Rad;
            main?.Play();
        }
        yield return new WaitForSeconds(1.9f);
        anim.SetBool(noDamageSpinAttackHash, false);
        anim.SetBool(spinAttackHash, true);
        _rb.linearVelocityX = dir * 6.3f;
        yield return new WaitForSeconds(3.4f);
        Debug.Log("공격 4 끝");
        _rb.linearVelocityX = 0;
        anim.SetBool(spinAttackHash, false);
        anim.SetBool(spinAttackEndHash, true);
    }
    //기즈모--------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closeAttackRange);

        float dir = isFacingRight ? 1f : -1f;

        Vector3 center = transform.position + Vector3.right * 3f * dir;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((transform.position + Vector3.right * 2.8f * dir), new Vector3(5f, 2, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((transform.position + Vector3.right * 3.3f * dir + Vector3.down * 0.2f), new Vector3(4f, 1.5f, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+Vector3.up*0.3f+Vector3.left*0.1f, 2f);
    }
    //기즈모--------------------------------------------------------
    
    //오버랩--------------------------------------------------------
    public void BigCloudOverLap()
    {
        float dir = isFacingRight ? 1f : -1f;

        Vector3 center = transform.position + Vector3.right * 2.8f * dir;

        Collider2D hit = Physics2D.OverlapBox(center, new Vector3(5f, 1.5f, 0), 0f, playerLayer);

        Debug.Log("플레이어 맞음 : Bigcloud");
        hit?.GetComponent<IDamageable>()?.GetDamage(bossData.spinAttackDamage, gameObject);
    }

    public void SmallCloudOverLap()
    {
        float dir = isFacingRight ? 1f : -1f;

        Vector3 center = transform.position + Vector3.right * 3.3f * dir + Vector3.down * 0.2f;

        Collider2D hit = Physics2D.OverlapBox(center, new Vector3(5.5f, 2, 0), 0f, playerLayer);
        
        Debug.Log("플레이어 맞음 : SmallCloud");
        hit?.GetComponent<IDamageable>()?.GetDamage(bossData.spinAttackDamage, gameObject);
    }

    public void SpinAttackOverLap()
    {
        Vector3 center = transform.position + Vector3.up * 0.3f + Vector3.left * 0.1f;

        Collider2D hit = Physics2D.OverlapCircle(center, 2f, playerLayer);

        Debug.Log("플레이어 맞음 : SpinAttack");
        hit?.GetComponent<IDamageable>()?.GetDamage(bossData.spinAttackDamage, gameObject);
    }
}

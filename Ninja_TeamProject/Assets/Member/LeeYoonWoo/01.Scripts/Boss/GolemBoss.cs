using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class GolemBoss : Boss//, IDamageable
{
    public float patternDelay = 5f;
    public float closeAttackRange = 5.0f;

    private Transform playerPos;
    private PlayerController player;
    private bool isAttacking = false;
    private CameraShake cs;

    Rigidbody2D _rb;

    //----------------------
    int bigCloudHash;
    int smallCloudHash;
    int shockWaveHash;
    int spinAttackHash;
    int spinAttackEndHash;
    int turnLeftHash;
    int noDamageSpinAttackHash;
    //----------------------

    protected override void Start()
    {
        base.Start();

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        cs = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //---------------------------------
        bigCloudHash = Animator.StringToHash("BigCloud");
        smallCloudHash = Animator.StringToHash("SmallCloud");
        shockWaveHash = Animator.StringToHash("ShockWave");
        spinAttackHash = Animator.StringToHash("SpinAttack");
        spinAttackEndHash = Animator.StringToHash("SpinAttackEnd");
        turnLeftHash = Animator.StringToHash("TurnLeft");
        noDamageSpinAttackHash = Animator.StringToHash("NoDamageSpinAttack");
        //---------------------------------

        StartCoroutine(BossThinkRoutine());
    }

    public override void Attack()
    {
        
    }

    void Update()
    {
        //Flip();
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
    private IEnumerator TurnAround()
    {
        float dir = playerPos.position.x - transform.position.x;

        if (dir > 0 && transform.localScale.x < 0)
        {
            anim.SetTrigger(turnLeftHash);
            yield return new WaitForSeconds(1.82489f);
            this.transform.localScale = Vector3.one;
        }
        else if (dir < 0 && transform.localScale.x > 0)
        {
            anim.SetTrigger(turnLeftHash);
            yield return new WaitForSeconds(1.82489f);
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void TurnRight()
    {
        transform.localScale = Vector3.one;
    }
    public void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
    private IEnumerator Pattern1_BigCloud()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 1");
        anim.SetBool(bigCloudHash, true);
        yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(3f);
    }
    private IEnumerator Pattern2_SmallCloud()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 2");
        anim.SetBool(smallCloudHash, true);

        yield return new WaitForSeconds(2f);
    }
    private IEnumerator Pattern3_ShockWave()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 3");
        anim.SetBool(shockWaveHash, true);
        yield return new WaitForSeconds(3f);

    }
    private IEnumerator Pattern4_SpinAttack()
    {
        yield return StartCoroutine(TurnAround());
        Debug.Log("공격 4 시작");
        float dir = (playerPos.position - transform.position).normalized.x;
        anim.SetBool(noDamageSpinAttackHash, true);
        yield return new WaitForSeconds(3.8f);
        anim.SetBool(noDamageSpinAttackHash, false);
        anim.SetBool(spinAttackHash, true);
        _rb.linearVelocityX = dir * 4.3f;
        yield return new WaitForSeconds(3.5f);
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

        float dir = transform.localScale.x > 0 ? 1f : -1f;

        Vector3 center = transform.position + Vector3.right * 3f * dir;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((transform.position + Vector3.right * 2.8f * dir), new Vector3(5f, 2, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((transform.position + Vector3.right * 3.3f * dir + Vector3.down * 0.2f), new Vector3(4f, 1.5f, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+Vector3.up*0.3f+Vector3.left*0.1f, 1.5f);
    }
    //기즈모--------------------------------------------------------
    
    //오버랩--------------------------------------------------------
    public void BigCloudOverLap()
    {
        float dir = transform.localScale.x > 0 ? 1f : -1f;

        Vector3 center = transform.position + Vector3.right * 2.8f * dir;

        Collider2D hit = Physics2D.OverlapBox(
            center,
            new Vector3(5f, 1.5f, 0),
            0f,
            playerLayer
        );

        if (hit != null && !hit.GetComponent<PlayerController>().PlayerHit)
        {
            Debug.Log("플레이어 맞음 : Bigcloud");
            hit.GetComponent<HealthSystem>().GetDamage(1, gameObject);
            StartCoroutine(hit.GetComponent<PlayerController>().PlayerHited());
        }
    }

    public void SmallCloudOverLap()
    {
        float dir = transform.localScale.x > 0 ? 1f : -1f;

        Vector3 center = transform.position + Vector3.right * 3.3f * dir + Vector3.down * 0.2f;

        Collider2D hit = Physics2D.OverlapBox(
            center,
            new Vector3(5.5f, 2, 0),
            0f,
            playerLayer
        );

        if (hit != null && !hit.GetComponent<PlayerController>().PlayerHit)
        {
            Debug.Log("플레이어 맞음 : SmallCloud");
            hit.GetComponent<HealthSystem>().GetDamage(1, gameObject);
            StartCoroutine(hit.GetComponent<PlayerController>().PlayerHited());
        }
    }
    public void SpinAttackOverLap()
    {
        Vector3 center = transform.position + Vector3.up * 0.3f + Vector3.left * 0.1f;

        Collider2D hit = Physics2D.OverlapCircle(
            center,
            1.5f,
            playerLayer
        );

        if (hit != null && !hit.GetComponent<PlayerController>().PlayerHit)
        {
            Debug.Log("플레이어 맞음 : SpinAttack");

            hit.GetComponent<HealthSystem>().GetDamage(1 , gameObject);
            StartCoroutine(hit.GetComponent<PlayerController>().PlayerHited());
        }
    }
}

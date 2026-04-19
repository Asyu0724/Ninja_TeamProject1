using System.Collections;
using UnityEngine;

public class GolemBoss : Boss
{
    public float patternDelay = 5f;
    public float closeAttackRange = 5.0f;

    private Transform player;
    private bool isAttacking = false;

    //----------------------
    int bigCloudHash;
    int smallCloudHash;
    int shockWaveHash;
    int spinAttackHash;
    int spinAttackEndHash;
    //----------------------

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        //---------------------------------
        bigCloudHash = Animator.StringToHash("BigCloud");
        smallCloudHash = Animator.StringToHash("SmallCloud");
        shockWaveHash = Animator.StringToHash("ShockWave");
        spinAttackHash = Animator.StringToHash("SpinAttack");
        spinAttackEndHash = Animator.StringToHash("SpinAttackEnd");
        //---------------------------------

        StartCoroutine(BossThinkRoutine());
    }

    public override void Attack()
    {
        
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
                ChooseNextPattern();
            }

            yield return null;
        }
    }

    private void ChooseNextPattern()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= closeAttackRange)
        {
            int randomPattern = UnityEngine.Random.Range(0, 2);
            if (randomPattern == 0) StartCoroutine(Pattern1_BigCloud());
            if (randomPattern == 1) StartCoroutine(Pattern2_SmallCloud());
        }

        else
        {
            int randomPattern = UnityEngine.Random.Range(2, 4);
            if (randomPattern == 2) StartCoroutine(Pattern3_ShockWave());
            if (randomPattern == 3) StartCoroutine(Pattern4_SpinAttack());
        }
    }

    private IEnumerator Pattern1_BigCloud()
    {
        Debug.Log("공격 1");
        anim.SetBool(bigCloudHash, true);

        yield return new WaitForSeconds(5f);
    }
    private IEnumerator Pattern2_SmallCloud()
    {
        Debug.Log("공격 2");
        anim.SetBool(smallCloudHash, true);

        yield return new WaitForSeconds(2f);
    }
    private IEnumerator Pattern3_ShockWave()
    {
        Debug.Log("공격 3");
        anim.SetBool(shockWaveHash, true);

        yield return new WaitForSeconds(3f);
    }
    private IEnumerator Pattern4_SpinAttack()
    {
        Debug.Log("공격 4 시작");
        anim.SetBool(spinAttackHash, true);
        yield return new WaitForSeconds(3f);
        Debug.Log("공격 4 끝");
        anim.SetBool(spinAttackHash, false);
        anim.SetBool(spinAttackEndHash, true);
    }

    private void OnDrawGizmos()
    {
        // 보스의 위치를 기준으로 closeAttackRange만큼의 원을 녹색으로 그립니다.
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closeAttackRange);
    }
}

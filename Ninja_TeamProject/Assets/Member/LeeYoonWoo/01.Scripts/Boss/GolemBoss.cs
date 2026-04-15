using System.Collections;
using UnityEngine;

public class GolemBoss : Boss
{
    public float patternDelay = 1.5f;
    public float closeAttackRange = 5.0f;

    private Transform player;
    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(BossThinkRoutine());
    }

    public override void Attack()
    {
        
    }

    private IEnumerator BossThinkRoutine()
    {
        while (currentHealth > 0)
        {
            if (!isAttacking)
            {
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

        }
    }
}

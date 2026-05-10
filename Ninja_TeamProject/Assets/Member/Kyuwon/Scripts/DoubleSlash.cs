using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoubleSlash : MonoBehaviour
{
    [SerializeField] private Vector2 attackRange;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Animator animator;
    private float _normalAttackCool = 1.5f;
    private float _timer = 0f;
    private bool _canNormalAttack;
    
    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        bool canSlashRange = Physics2D.OverlapBox(transform.position,attackRange, whatIsPlayer);
        if (canSlashRange && _timer >= _normalAttackCool)
        {
            StartCoroutine(SlashRoutine());
            _timer = 0f;
        }
        else
        {
        }
    }

    private IEnumerator SlashRoutine()
    {
        int criticalSlash = Random.Range(0, 5);

        if (criticalSlash < 1)
        {
            Debug.Log("어류겐");
        }
        else
        {
            Debug.Log("휭");
            animator.SetTrigger("Slash");
        }
        yield return new WaitForSeconds(1.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackRange);
    }
}

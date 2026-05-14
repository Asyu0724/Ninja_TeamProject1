using UnityEngine;
using System.Collections;
using Member.Kyuwon.SBossSO;

public class BossSlash : MonoBehaviour
{
    public SBossData bossData;

    private Animator _animator;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Slash()
    {
        Collider2D isHit = Physics2D.OverlapBox(transform.position, bossData.NormalRange, 0,whatIsPlayer);

        if (isHit != null)
        {
            float timeStamp = Time.time;
            
            _animator.SetTrigger("Slash");
            StartCoroutine(CanAttack());
        }
    }
    
    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(1.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bossData.NormalRange);
    }
}

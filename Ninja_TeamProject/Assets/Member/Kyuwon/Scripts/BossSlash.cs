using UnityEngine;
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
            _animator.SetTrigger("Slash");
            Debug.Log("데미지 드갔다 야르~~");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bossData.NormalRange);
    }
}

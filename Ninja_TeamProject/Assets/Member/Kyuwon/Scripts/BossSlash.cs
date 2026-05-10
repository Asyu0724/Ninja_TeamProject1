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
        _animator.SetTrigger("Slash");

        Collider2D hit = Physics2D.OverlapBox(transform.position, bossData.NormalRange, 0f,whatIsPlayer);

        if (hit != null)
        {
            Debug.Log("데미지 드갔다 야르~~");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bossData.NormalRange);
    }
}

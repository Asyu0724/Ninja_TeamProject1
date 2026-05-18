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
        float offsetDistance = bossData.NormalRange.x * 0.5f;
        Vector2 SlashPosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);
        
        Collider2D isHit = Physics2D.OverlapBox(SlashPosition, bossData.NormalRange, 0,whatIsPlayer);

        if (isHit != null)
        {
            float timeStamp = Time.time;
            
            _animator.SetTrigger("Slash");
        }
    }
}

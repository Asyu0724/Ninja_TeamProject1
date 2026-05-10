using UnityEngine;
using System;
using Member.Kyuwon.SBossSO;

public class SBoss : MonoBehaviour
{
    private BossSlash _slash;
    [SerializeField] private LayerMask whatIsPlayer;
    private Action _SBossSkill;
    public SBossData bossData;
    public bool isAttacking = false;

    void Awake()
    {
        _slash = GetComponent<BossSlash>();
    }

    void Update()
    {
        bool SlashRange = Physics2D.OverlapBox(transform.position,bossData.NormalRange, 0f,whatIsPlayer);
        if (SlashRange != false && isAttacking == false)
        {
            _SBossSkill = _slash.Slash; 
            _bossSkills();
            isAttacking = true;
        }
    }
    
    public void _bossSkills()
    {
        if (_SBossSkill != null)
        {
            _SBossSkill?.Invoke();
        }
    }
}

using UnityEngine;
using System;
using Member.Kyuwon.SBossSO;

public class SBoss : MonoBehaviour
{
    private Action _SBossSkill;
    
    private BossSlash _slash;
    private BossFinisher _finisher;
    [SerializeField] private LayerMask whatIsPlayer;
    public SBossData bossData;
    public bool isAttacking = false;

    void Awake()
    {
        _slash = GetComponent<BossSlash>();
        _finisher = GetComponent<BossFinisher>();
    }

    void Update()
    {
        bool SlashRange = Physics2D.OverlapBox(transform.position,bossData.NormalRange, 0,whatIsPlayer);
        bool FinisherRange = Physics2D.OverlapBox(transform.position,bossData.FinisherRange, 0,whatIsPlayer);
        
        if (FinisherRange != false && isAttacking == false)
        {
            _SBossSkill = _finisher.Finisher;
            _bossSkills();
            isAttacking = true;
        }
        
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

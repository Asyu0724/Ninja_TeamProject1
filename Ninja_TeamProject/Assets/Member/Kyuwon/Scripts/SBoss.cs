using UnityEngine;
using System;
using System.Collections;
using Member.Kyuwon.SBossSO;

public class SBoss : MonoBehaviour
{
    private Action _SBossSkill;
    
    private BossSlash _slash;
    private BossCharge _charge;
    private BossFinisher _finisher;
    [SerializeField] private LayerMask whatIsPlayer;
    public SBossData bossData;
    public bool isAttacking = false;

    void Awake()
    {
        _slash = GetComponent<BossSlash>();
        _charge = GetComponent<BossCharge>();
        _finisher = GetComponent<BossFinisher>();
        bossData.CanCharging = true;
        bossData.CanFinisher = true;
        bossData.CanNormal = true;
    }

    void Update()
    {
        float SlashDistance = bossData.NormalRange.x * 0.5f;
        Vector2 SlashPosition = (Vector2)transform.position + ((Vector2)transform.right * SlashDistance);
        
        float ChargeDistance = bossData.ChargeRange.x * 0.5f;
        Vector2 ChargePosition = (Vector2)transform.position + ((Vector2)transform.right * ChargeDistance);
        
        bool SlashRange = Physics2D.OverlapBox(SlashPosition,bossData.NormalRange, 0,whatIsPlayer);
        bool FinisherRange = Physics2D.OverlapBox(transform.position,bossData.FinisherRange, 0,whatIsPlayer);
        bool ChargeRange = Physics2D.OverlapBox(ChargePosition, bossData.ChargeRange, 0, whatIsPlayer);

        if (FinisherRange != false && isAttacking == false && bossData.CanFinisher == true)
        {
            _SBossSkill = _finisher.Finisher;
            isAttacking = true;
            bossData.CanFinisher = false;
            _bossSkills();
            StartCoroutine(IsAttacking());
            StartCoroutine(FinisherCool());
        }
        
        if (SlashRange != false && isAttacking == false && bossData.CanNormal == true)
        {
            _SBossSkill = _slash.Slash;
            isAttacking = true;
            bossData.CanNormal = false;
            _bossSkills();
            StartCoroutine(IsAttacking());
            StartCoroutine(NormalCool());
        }

        if (ChargeRange != false && isAttacking == false && bossData.CanCharging == true)
        {
            _SBossSkill = _charge.Charge;
            isAttacking = true;
            bossData.CanCharging = false;
            _bossSkills();
            StartCoroutine(IsAttacking());
            StartCoroutine(ChargingCool());
        }
        
    }

    IEnumerator IsAttacking()
    {
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }
    
    IEnumerator FinisherCool()
    {
        yield return new WaitForSeconds(bossData.FinisherCool);
        bossData.CanFinisher = true;
    }
    
    IEnumerator NormalCool()
    {
        yield return new WaitForSeconds(bossData.NormalCool);
        bossData.CanNormal = true;
    }

    IEnumerator ChargingCool()
    {
        yield return new WaitForSeconds(bossData.ChargingCool);
        bossData.CanCharging = true;
    }
    
    public void _bossSkills()
    {
        if (_SBossSkill != null)
        {
            _SBossSkill?.Invoke();
        }
    }
}

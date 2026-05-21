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

    private WaitForSeconds attacking = new WaitForSeconds(2.0f);

    private BossMove _bossMove;
    [SerializeField] private LayerMask whatIsPlayer;
    public SBossData bossData;
    public bool isAttacking = false;

    void Awake()
    {
        _bossMove = GetComponent<BossMove>();
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
            StartCoroutine(IsAttacking());
            StartCoroutine(FinisherCool());
        }
        
        if (SlashRange == true && isAttacking == false && bossData.CanNormal == true)
        {
            Debug.Log(_slash);
            _SBossSkill = _slash.Slash;
            isAttacking = true;
            bossData.CanNormal = false;
            StartCoroutine(IsAttacking());
            StartCoroutine(NormalCool());
        }

        if (ChargeRange != false && isAttacking == false && bossData.CanCharging == true)
        {
            Debug.Log("연속베기 준비!");
            _SBossSkill = _charge.Charge;
            isAttacking = true;
            bossData.CanCharging = false;
            StartCoroutine(IsAttacking());
            StartCoroutine(ChargingCool());
        }
        
    }
    
    IEnumerator IsAttacking()
    {
        yield return attacking;
        Debug.Log("연속 베기");
        _bossSkills();
        isAttacking = false;
        BossMove.instance.MoveSpeed = bossData.speed;
    }
    
    IEnumerator FinisherCool()
    {
        yield return bossData.FinisherCool;
        bossData.CanFinisher = true;
    }
    
    IEnumerator NormalCool()
    {
        yield return bossData.NormalCool;
        bossData.CanNormal = true;
    }

    IEnumerator ChargingCool()
    {
        yield return bossData.ChargingCool;
        bossData.CanCharging = true;
    }
    
    public void _bossSkills()
    {
         _SBossSkill?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        
        float offsetDistance = bossData.NormalRange.x * 0.5f;
        Vector2 SlashPosition = (Vector2)transform.position + ((Vector2)transform.right * offsetDistance);
        
        Gizmos.DrawWireCube(SlashPosition, bossData.NormalRange);
    }

    public void SlashOverlap()
    {
        _slash.SlashOverLap();
    }

    public void FinisherOverLap()
    {
        _finisher.FinisherOverLap();
    }

    public void ChargeOverLap()
    {
        _charge.ChargeOverLap();
    }

    public void ChargeChange()
    {
        _bossMove.ChargeChange();
    }
}

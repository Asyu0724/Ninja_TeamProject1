using System;
using System.Collections;
using System.Xml.Schema;
using Member.Choijeongyun._01.Scripts.Func;
using Member.KimJoonYoung._01.Scripts.Agent;
using Member.KimJoonYoung._01.Scripts.Hp;
using Member.KimJoonYoung._01.Scripts.UI.Boss;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private BossRenderer bossRenderer;
    [SerializeField] private BossHealthBarUI healthBarUI;
    [SerializeField] private BossMover bossMover;
    [SerializeField] private CJY_AudioManager bossAudio;
    public UnityEvent OnDamage;

    private int _bossHealth;
    public bool IsDeath { get; private set; }
    public bool IsCharge { get; private set; }
    
    private int _canCharge = 2;


    private void Start()
    {
        healthBarUI.InitHealthUI(_bossHealth, maxHealth);
        _bossHealth = maxHealth;
        IsDeath = false;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        if (IsDeath || IsCharge || bossMover.IsJump) return;
        _bossHealth -= damage;
        OnDamage?.Invoke();
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
        healthBarUI.UpdateHealthUI(_bossHealth);
        bossRenderer.StartCoroutine("Attacked");
        if (_bossHealth <= 10 && _canCharge > 0)
        {
            ChargeHP();
        }

        if (_bossHealth <= 0)
        {
            bossAudio.PlaySFX(7,0.1f);
            IsDeath = true;
        }
    }

    public void ChargeHP()
    {
        StartCoroutine(ChargeHPCorutine());
    }

    public IEnumerator ChargeHPCorutine()
    {
        if (bossMover.NotOtherSkill) 
            yield return StartCoroutine(OtherSkill());
        bossAudio.PlaySFX(6,0);
        IsCharge = true;
        _canCharge--;
        bossRenderer.ChargeStart();
        StartCoroutine(HP());
    }

    private IEnumerator HP()
    {
        float chargeTime = 0;
        while (true)
        {
            _bossHealth += 3;
            chargeTime += 0.5f;
            _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
            healthBarUI.UpdateHealthUI(_bossHealth);
            if (chargeTime >= 3)
            {
                IsCharge = false;
                bossRenderer.ChargeEnd();
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator OtherSkill()
    {
        Debug.Log("노놉! 지금은 충전 못한다!");
        yield return new WaitUntil(() => bossMover.NotOtherSkill == false);
    }




}

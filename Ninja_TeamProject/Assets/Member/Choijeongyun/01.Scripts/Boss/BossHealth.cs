using System.Collections;
using Member.KimJoonYoung._01.Scripts.UI.Boss;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform bloodParticle;
    [SerializeField] private int maxHealth;
    [SerializeField] private BossRenderer bossRenderer;
    [SerializeField] private BossHealthBarUI healthBarUI;
    [SerializeField] private BossMover bossMover;
    public UnityEvent OnDamage;
    public UnityEvent OnDeath;

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
        bloodParticle.position = transform.position;
        OnDamage?.Invoke();
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
        healthBarUI.UpdateHealthUI(_bossHealth);
        bossRenderer.StartCoroutine("Attacked");
        if ((float)_bossHealth / maxHealth <= 0.3f && _canCharge > 1 && !IsCharge)
        {
            IsCharge = true;
            ChargeHP();
        }
        else if ((float)_bossHealth / maxHealth <= 0.15f && _canCharge > 0 && !IsCharge)
        {
            IsCharge = true;
            ChargeHP();
        }

        if (_bossHealth <= 0)
        {
            OnDeath?.Invoke();
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.BossCharge, 9);
        bossRenderer.ChargeStart();
        StartCoroutine(HP());
    }

    private IEnumerator HP()
    {
        float chargeTime = 0;
        while (true)
        {
            _bossHealth += 6 * _canCharge;
            chargeTime += 0.5f;
            _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
            healthBarUI.UpdateHealthUI(_bossHealth);
            if (chargeTime >= 3)
            {
                IsCharge = false;
                bossRenderer.ChargeEnd();
                _canCharge--;
                
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
    
using System.Collections;
using System.Xml.Schema;
using Member.KimJoonYoung._01.Scripts.Agent;
using Member.KimJoonYoung._01.Scripts.Hp;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private BossRenderer _renderer;
    [SerializeField] private HealthBarUI _healthBarUI;
    
    private int _bossHealth;
    public bool IsDeath { get; private set; }
    public bool IsCharge { get; private set; }
    private int _canCharge = 2;


    private void Start()
    {
        _healthBarUI.InitHealthUI(maxHealth);
        _bossHealth = maxHealth;
        IsDeath = false;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        _bossHealth -= damage;
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
        _healthBarUI.UpdateHealthUI(_bossHealth);
        _renderer.StartCoroutine("Attacked");
        if (_bossHealth <= 10 && _canCharge > 0)
        {
            ChargeHP();
        }
        if (_bossHealth <= 0) IsDeath = true;
    }

    public void ChargeHP()
    {
        IsCharge = true;
        _canCharge--;
        _renderer.ChargeStart();
        StartCoroutine(HP());
    }

    private IEnumerator HP()
    {
        while (true)
        {
            _bossHealth += 3;
            _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
            _healthBarUI.UpdateHealthUI(_bossHealth);
            if (_bossHealth >= maxHealth)
            {
                IsCharge = false;
                _renderer.ChargeEnd();
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    
    

}

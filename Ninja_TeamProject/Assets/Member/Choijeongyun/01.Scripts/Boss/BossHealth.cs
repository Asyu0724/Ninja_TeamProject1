using System.Xml.Schema;
using Member.KimJoonYoung._01.Scripts.Agent;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private BossRenderer _renderer;
    
    private int _bossHealth;
    public bool IsDeath { get; private set; }
    public bool IsCharge { get; private set; }
    private int _canCharge = 10;


    private void Start()
    {
        _bossHealth = maxHealth;
        IsDeath = false;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        _bossHealth -= damage;
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
        _renderer.StartCoroutine("Attacked");
        if (_bossHealth <= 0) IsDeath = true;
    }

    public void ChargeHP(bool value)
    {
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
        if (_canCharge <= 0) IsCharge = false;
        _canCharge--;
    }

}

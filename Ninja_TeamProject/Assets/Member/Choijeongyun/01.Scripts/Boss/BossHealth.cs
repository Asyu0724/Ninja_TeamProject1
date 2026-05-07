using System.Xml.Schema;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int _bossHealth;
    public bool IsDeath { get; private set; }
    public bool IsCharge { get; private set; }
    private int _canCharge = 10;


    private void Start()
    {
        _bossHealth = maxHealth;
        IsDeath = false;
    }

    public void ChangeHealth(int value)
    {
        _bossHealth -= value;
        if (_bossHealth <= 0) IsDeath = true;
    }

    public void ChargeHP(bool value)
    {
        IsCharge = value;
        if (_canCharge <= 0) IsCharge = false;
        _canCharge--;
    }

}

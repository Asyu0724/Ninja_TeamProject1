using System.Xml.Schema;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 20;
    private int _bossHealth;
    public bool _isDeath { get; private set; }
    public bool _isCharge { get; private set; }
    private int _canCharge = 10;


    private void Start()
    {
        _bossHealth = _maxHealth;
        _isDeath = false;
    }

    public void ChangeHealth(int value)
    {
        _bossHealth -= value;
        if (_bossHealth <= 0) _isDeath = true;
    }

    public void ChargeHP(bool value)
    {
        _isCharge = value;
        if (_canCharge <= 0) _isCharge = false;
        _canCharge--;
    }

}

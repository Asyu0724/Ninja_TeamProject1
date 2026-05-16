
using Member.KimJoonYoung._01.Scripts.Hp;
using UnityEngine;

public class Min_BossHealth : MonoBehaviour
{
    
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private Min_BossRenderer _renderer;
    [SerializeField] private HealthBarUI _healthBarUI;
    
    private int _bossHealth;
    public bool IsDeath { get; private set; }

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
        if (_bossHealth <= 0) IsDeath = true;
    }
}

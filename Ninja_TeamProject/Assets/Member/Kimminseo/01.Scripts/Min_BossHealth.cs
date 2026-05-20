using Member.KimJoonYoung._01.Scripts.Hp;
using UnityEngine;

public class Min_BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private HealthBarUI _healthBarUI;
    
    private int _bossHealth;
    public bool IsDeath { get; private set; }

    private void Start()
    {
        _bossHealth = maxHealth;
        IsDeath = false;
        _healthBarUI.InitHealthUI(maxHealth);
        _healthBarUI.UpdateHealthUI(_bossHealth);
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        if (IsDeath) return;

        _bossHealth -= damage;
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
        _healthBarUI.UpdateHealthUI(_bossHealth);

        if (_bossHealth <= 0)
        {
            IsDeath = true;
        }
    }
}
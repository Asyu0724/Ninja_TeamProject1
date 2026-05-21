using System;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour, IDamageable
{
    public interface IDamageable
    {
        public void GetDamage(int damage, GameObject dealer);
    }

    public int health;
    public int maxHealth = 30;
    [field:SerializeField] public BossHealthUI healthUI {  get; private set; }
    public event Action OnDamaged;
    public event Action Dead;
    public static BossHealthSystem instance;
    private bool _invNow;

    private void Awake()
    {
        health = maxHealth;
        instance = this;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        health -= damage;
        healthUI.UpdateBossHealthBar();
        health = Mathf.Clamp(health, 0, maxHealth);
        OnDamaged?.Invoke();
        Debug.Log("나 맞음ㅠ");
        //UI와 연동
        if (health <= 0)
        {
            // 보스 사망
            Dead?.Invoke();
        }
    }
}

using System;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour, IDamageable
{
    public interface IDamageable
    {
        public void GetDamage(int damage, GameObject dealer);
    }

    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 30;
    public event Action OnDamaged;
    public event Action Dead;
    private bool _invNow;

    private void Awake()
    {
        health = maxHealth;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        health -= damage;
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

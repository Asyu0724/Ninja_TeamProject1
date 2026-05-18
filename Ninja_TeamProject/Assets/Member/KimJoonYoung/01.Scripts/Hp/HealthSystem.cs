using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public interface IDamageable
{
    public void GetDamage(int damage, GameObject dealer);
}

public class HealthSystem : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float InvTime {get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field:SerializeField] public int MaxHealth {get; private set;}
    public event Action OnDamaged;
    public event Action Dead;
    private bool _invNow;

    private void Awake()
    {
        InitHealth();
    }

    private void InitHealth()
    {
        Health = MaxHealth;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        if (Health <= 0) return;
        
        if (!_invNow)
        {
            StartCoroutine(InvNow());
            Health -= damage;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
            OnDamaged?.Invoke();
            if (Health <= 0)
            {
                Dead?.Invoke();
            }
        }
    }

    IEnumerator InvNow()
    {
        _invNow = true;
        yield return new WaitForSeconds(InvTime);
        _invNow = false;
    }
}

using System;
using UnityEngine;

public interface IDamageable
{
    public void GetDamage(int damage, GameObject dealer);
}

public class HealthSystem : MonoBehaviour, IDamageable
{
    public event Action OnDamaged;

    [field: SerializeField] public int Health { get; private set; }
    [SerializeField] private int _maxHealth;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, _maxHealth);
        OnDamaged?.Invoke();

        if (Health <= 0)
        {
            //죽음
            Destroy(gameObject);
        }
    }
}

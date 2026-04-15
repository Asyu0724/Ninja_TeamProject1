using JetBrains.Annotations;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public string bossName;
    public float maxHealth;
    public float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

    }
    
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public abstract void Attack();
    
}

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

/*    protected Collider2D[] CheckCircleOverlap(Vector2 position, float range)
    {
        return Physics2D.overlapCircleAll();
    }*/

    public abstract void Attack();
    
}

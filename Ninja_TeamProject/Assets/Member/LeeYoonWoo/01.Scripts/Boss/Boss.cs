using JetBrains.Annotations;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public string bossName;
    public float maxHealth;
    public float currentHealth;

    public LayerMask playerLayer;

    protected Animator anim;

    int hitCount = 0;
    [SerializeField] int hitsPerHp = 4;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();

        UIManager.Instance.BossHealthUI.InitHealthUI((int)maxHealth);
    }

    public virtual void TakeDamage(float damageAmount)
    {
        hitCount += (int)damageAmount;

        UIManager.Instance.BossHealthUI.Shake();

        if (hitCount >= hitsPerHp)
        {
            hitCount = 0;
            currentHealth--;

            UIManager.Instance.BossHealthUI.UpdateHealthUI((int)currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }



    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected Collider2D[] CheckCircleOverlap(Vector2 position, float range)
    {
        return Physics2D.OverlapCircleAll(position, range, playerLayer);
    }

    protected Collider2D[] CheckBoxOverlap(Vector2 position, Vector2 size)
    {
        return Physics2D.OverlapBoxAll(position, size, 0f, playerLayer);
    }

    public abstract void Attack();
    
}

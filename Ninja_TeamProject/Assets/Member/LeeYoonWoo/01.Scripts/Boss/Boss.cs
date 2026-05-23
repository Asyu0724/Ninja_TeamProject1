using System.Diagnostics;
using JetBrains.Annotations;
using Member.KimJoonYoung._01.Scripts.UI.Boss;
using Member.LeeYoonWoo.SO;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public abstract class Boss : MonoBehaviour , IDamageable
{
    public string bossName;
    public float maxHealth;
    public float currentHealth;

    public LayerMask playerLayer;
    public UnityEvent OnHit;

    protected Animator anim;

    int hitCount = 0;
    [SerializeField] private BossHealthBarUI bossHealthSlider;
    [SerializeField] protected BossDataSO bossData;
    // [SerializeField] int hitsPerHp = 1;

    protected virtual void Awake()
    {
        maxHealth = bossData.maxHealth;
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();

        // UIManager.Instance.BossHealthUI.InitHealthUI((int)maxHealth);
    }

    protected virtual void Start()
    {
        bossHealthSlider.InitHealthUI(currentHealth ,maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log(damageAmount);
        OnHit?.Invoke();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.GolemDamage , 6);
        currentHealth-=damageAmount;
        bossHealthSlider.UpdateHealthUI(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        
    }

    protected Collider2D[] CheckCircleOverlap(Vector2 position, float range)
    {
        return Physics2D.OverlapCircleAll(position, range, playerLayer);
    }

    protected Collider2D[] CheckBoxOverlap(Vector2 position, Vector2 size)
    {
        return Physics2D.OverlapBoxAll(position, size, 0f, playerLayer);
    }
    

    public void GetDamage(int damage, GameObject dealer)
    {
        TakeDamage(damage);
    }
}

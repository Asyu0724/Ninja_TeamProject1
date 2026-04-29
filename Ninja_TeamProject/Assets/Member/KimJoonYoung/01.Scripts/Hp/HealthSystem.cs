using System;
using System.Collections;
using UnityEngine;

public interface IDamageable
{
    public void GetDamage(int damage, GameObject dealer);
}

public class HealthSystem : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float InvTime {get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [SerializeField] private int _maxHealth;
    public event Action OnDamaged;
    private bool _invNow;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void GetDamage(int damage, GameObject dealer)
    {
        if (!_invNow)
        {
            StartCoroutine(InvNow());
            if (!GameManager.Instance.player.PlayerHit)
            {
                Health -= damage;
                Health = Mathf.Clamp(Health, 0, _maxHealth);
                OnDamaged?.Invoke();
            }

            if (Health <= 0)
            {
                //죽음
                Destroy(gameObject);
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

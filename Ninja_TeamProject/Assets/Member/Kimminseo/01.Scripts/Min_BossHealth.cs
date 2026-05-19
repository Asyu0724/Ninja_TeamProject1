using Member.KimJoonYoung._01.Scripts.Agent;
using Member.KimJoonYoung._01.Scripts.Hp;
using UnityEngine;

public class Min_BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private Min_BossRenderer _renderer;
    [SerializeField] private HealthBarUI _healthBarUI;
    [SerializeField] private AgentAttack _agentAttack;
    [SerializeField] private float invTime = 0.5f; 
    private bool _wasOverlapping = false;

    private int _bossHealth;
    private bool _invNow;
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
        if (IsDeath || _invNow) return;

        _bossHealth -= damage;
        _bossHealth = Mathf.Clamp(_bossHealth, 0, maxHealth);
    
        Debug.Log(_bossHealth); 

        _healthBarUI.UpdateHealthUI(_bossHealth);

        if (_bossHealth <= 0)
        {
            IsDeath = true;
            return;
        }

        _invNow = true;
        Invoke(nameof(ResetInv), invTime);
    }

    private void Update()
    {
        if (IsDeath) return;

        Collider2D hit = Physics2D.OverlapBox(
            _agentAttack.transform.position + (Vector3)_agentAttack.offset,
            _agentAttack.boxSize,
            0f
        );

        bool isOverlappingNow = hit != null && hit.gameObject == gameObject;

        if (isOverlappingNow && !_wasOverlapping && !_invNow)
        {
            GetDamage(1, _agentAttack.gameObject);
        }
        _wasOverlapping = isOverlappingNow;

    }

    private void ResetInv()
    {
        _invNow = false;
    }
}
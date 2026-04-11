using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestEnemy : Agent
{
    private HealthSystem _healthSystem;
    private EnemyHealthBarUI _enemyHealthBarUI;
    private bool attacked;

    // Hash
    private int _isAttackedHash = Animator.StringToHash("Attacked");

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();
        _enemyHealthBarUI = GetComponentInChildren<EnemyHealthBarUI>();
    }

    private void Update()
    {
        _agentRenderer.SetBoolParam(_isAttackedHash, attacked);
    }

    public void AttackedNow()
    {
        _enemyHealthBarUI.HealthBarUpdate();
        attacked = true;
        StartCoroutine(Attacked());
    }

    IEnumerator Attacked()
    {
        yield return new WaitForSeconds(0.2f);
        attacked = false;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class TestEnemy : Agent
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private int _testenemyHP = 10;

    private bool attacked;

    // Hash
    private int _isAttackedHash = Animator.StringToHash("Attacked");

    private void Start()
    {
        hpText.text = ($"Enemy Bot HP : {_testenemyHP}");
    }

    private void Update()
    {
        _agentRenderer.SetBoolParam(_isAttackedHash, attacked);

        if (_testenemyHP < 1)
        {
            hpText.text = ($"Died");
            Destroy(gameObject);
        }
    }

    public void AttackedNow(bool atk)
    {
        _testenemyHP--;
        hpText.text = ($"Enemy Bot HP : {_testenemyHP}");
        attacked = atk;
        StartCoroutine(Attacked());
    }

    IEnumerator Attacked()
    {
        yield return new WaitForSeconds(0.2f);
        attacked = false;
    }
}

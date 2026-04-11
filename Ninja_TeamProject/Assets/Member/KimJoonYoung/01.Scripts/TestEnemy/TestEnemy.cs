using System.Collections;
using UnityEngine;

public class TestEnemy : Agent
{
    private bool attacked;

    private int _isAttackedHash = Animator.StringToHash("Attacked");

    private void Update()
    {
        _agentRenderer.SetBoolParam(_isAttackedHash, attacked);
    }

    public void AttackedNow(bool atk)
    {
        attacked = atk;
        StartCoroutine(Attacked());
    }

    IEnumerator Attacked()
    {
        yield return new WaitForSeconds(0.2f);
        attacked = false;
    }
}

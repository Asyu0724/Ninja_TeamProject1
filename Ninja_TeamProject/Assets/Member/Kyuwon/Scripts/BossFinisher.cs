using UnityEngine;
using System.Collections;
using Member.Kyuwon.SBossSO;

public class BossFinisher : MonoBehaviour
{
    public SBossData bossData;

    private int Skill;
    private Animator _animator;
    [SerializeField] private LayerMask whatIsPlayer;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    public void Finisher()
    {
        Collider2D isHit = Physics2D.OverlapBox(transform.position, bossData.FinisherRange, 0,whatIsPlayer);

        if (isHit != null)
        {
            Skill = Random.Range(0, 5);

            if (Skill < 3)
            {
                _animator.SetTrigger("Finisher");
            }
            else
            {
                _animator.SetTrigger("SFinisher");
            }
        }
    }

    public void FinisherOverLap()
    {
        BossMove.instance.MoveSpeed = 0f;
        Debug.Log("Finisher");
    }
}

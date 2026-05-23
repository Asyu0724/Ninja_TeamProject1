using System.Collections;
using System.Linq.Expressions;
using Member.KimJoonYoung._01.Scripts.Agent;
using UnityEngine;
using Random = UnityEngine.Random;

public class Min_BossRenderer : MonoBehaviour
{
    [SerializeField] private Min_BossMover bossMover;
    [SerializeField] private Min_BossSkill bossSkill;
    [SerializeField] private Min_BossHealth bossHealth;
    
    private Animator _anim;
    
    [SerializeField] private AgentRenderer agentRenderer;
    [SerializeField] private Transform playerTrm;

    private int _hashAttack1 = Animator.StringToHash("Attack1");
    private int _hashAttack2 = Animator.StringToHash("Attack2");
    private int _hashMoveX = Animator.StringToHash("moveX");
    private int _hashAttack3 = Animator.StringToHash("Attack3");
    private int _hashTeleport = Animator.StringToHash("Teleport");
    private int _hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetBool(_hashAttack1, bossMover.Attack1);
        _anim.SetFloat(_hashMoveX, bossMover.MoveX);
        _anim.SetBool(_hashAttack2, bossMover.Attack2);
        _anim.SetBool(_hashAttack3, bossMover.Attack3);
        _anim.SetBool(_hashTeleport, bossMover.Tp);
        _anim.SetBool(_hashDie, bossHealth.IsDeath);
    }

    public void AttackFin()
    {
        bossMover.SkillOff();
    }
    
    public void MoveToPlayer()
    {
        bossMover.MoveToPlayer();
    }

    
    // 규원 킴의 찌꺼기
    /*public void SetAttack2Fin()
    {
        _anim.SetBool(_hashAttack2Fin, true);
    }
    public void Attack2Start()
    {
        bossMover.Attack2 = true;
    }
    public void MoveColliderAttackStart()
    {
        _collider.offset = new Vector2(0, -3);
    }

    public void MoveColliderAttackEnd()
    {
        _collider.offset = new Vector2(0, -2);    }
    public void MoveColliderAttack2Start()
    {
        _collider.offset = new Vector2(0, 1.4f);
    }
    public void Disappear()
    {
        _collider.offset = new Vector2(0, -10);
    }

    public void DamageAttack1()
    {
        bossMover.Attack1OverLap();
    }
    
    public void DamageAttack2()
    {
        bossMover.Attack2OverLap();
    }
    
    public void DamageAttack3()
    {
        bossMover.Attack3OverLap();
    }*/

}


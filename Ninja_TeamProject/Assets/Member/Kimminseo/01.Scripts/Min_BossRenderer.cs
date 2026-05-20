using System.Collections;
using Member.KimJoonYoung._01.Scripts.Agent;
using UnityEngine;
using Random = UnityEngine.Random;

public class Min_BossRenderer : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    public Min_BossMover bossmover;
    public Min_BossHealth bossHealth;
    private Animator _anim;
    private SpriteRenderer _spriter;
    private int skill;
    [SerializeField] private AgentRenderer agentRenderer;
    [SerializeField] private Transform _playertrm;

    /*
    [SerializeField] private GameObject attack1Preview;
    [SerializeField] private GameObject attack2Preview;
    [SerializeField] private GameObject attack3Preview;
    [SerializeField] private GameObject[] attack3Knives; */

    private int HashAttack1    = Animator.StringToHash("Attack1");
    private int HashAttack2    = Animator.StringToHash("Attack2");
    private int HashAttack3    = Animator.StringToHash("Attack3");
    private int HashAttack1Fin = Animator.StringToHash("Attack1Finish");
    private int HashAttack2Fin = Animator.StringToHash("Attack2Finish");
    private int HashAttack3Fin = Animator.StringToHash("Attack3Finish");
    private int HashTeleport = Animator.StringToHash("Teleport");
    private int HashTeleportFinish = Animator.StringToHash("TeleportFinish");
    private int HashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        _anim    = GetComponent<Animator>();
        _spriter = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
        
        /*
        if(attack1Preview) attack1Preview.SetActive(false);
        if(attack2Preview) attack2Preview.SetActive(false);
        if(attack3Preview) attack3Preview.SetActive(false);
        foreach (var knife in attack3Knives)
        {
            if(knife) knife.SetActive(false);
        }*/
    }

    private void Update()
    {
        float distanceX = transform.position.x - _playertrm.position.x;
        if (Mathf.Abs(distanceX) > 1f)
            _spriter.flipX = distanceX > 0f;
        if (bossHealth.IsDeath == true)
        {
            StartCoroutine(Dead());
        }
    }

    private IEnumerator Dead()
    {
        _anim.SetTrigger(HashDie);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(1);
        _anim.speed = 1.0f;
        _anim.speed = 0.0f;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            ResetAll();

            skill = Random.Range(1, 4);

            switch (skill)
            {
                case 1:
                    yield return StartCoroutine(Teleport());
                    yield return StartCoroutine(DoAttack1());
                    break;
                case 2: yield return StartCoroutine(DoAttack2()); break;
                case 3: 
                    yield return StartCoroutine(Teleport());
                    yield return StartCoroutine(DoAttack3());
                    break;
            }

            ResetAll();
            if (bossHealth.IsDeath == true)
                break;
        }
    }


    private IEnumerator Teleport()
    {
        _anim.SetBool(HashTeleport, true); 
        yield return null;
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.SetBool(HashTeleportFinish, true);
        if (skill == 1)
            bossmover.attack1start = true;
        else if (skill == 3)
            bossmover.attack3start = true;
        _anim.SetBool(HashTeleport, false);
        yield return null;
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.SetBool(HashTeleportFinish, false);
    }

    private IEnumerator DoAttack1()
    {/*
        attack1Preview.SetActive(true); 
        yield return new WaitForSeconds(1.5f);
        attack1Preview.SetActive(false); */
        
        _anim.SetBool(HashAttack1, true);
        yield return null;
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        bossmover.attack1start = false;
        _anim.SetBool(HashAttack1, false);
        _anim.SetBool(HashAttack1Fin, true);
        yield return null;
        _anim.SetBool(HashAttack1Fin, false);
    }
    
    private IEnumerator DoAttack2()
    {/*
        attack2Preview.SetActive(true); 
        yield return new WaitForSeconds(1.5f);
        attack2Preview.SetActive(false); */
        
        _anim.SetBool(HashAttack2, true);
        yield return null;
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length); // 안전장치 추가
        bossmover.Attack2move = false;
        _anim.SetBool(HashAttack2, false);
        _anim.SetBool(HashAttack2Fin, false);
    }

    private IEnumerator DoAttack3()
    {/*
        attack3Preview.SetActive(true);
        foreach (var knife in attack3Knives) knife.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        
        attack3Preview.SetActive(false);
        foreach (var knife in attack3Knives) knife.SetActive(false);*/
        
        _anim.SetBool(HashAttack3, true);
        yield return null;
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        bossmover.attack3start = false;
        _anim.SetBool(HashAttack3, false);
        _anim.SetBool(HashAttack3Fin, true);
        yield return null;
        _anim.SetBool(HashAttack3Fin, false);
    }

    private void ResetAll()
    {
        _anim.SetBool(HashAttack1, false);
        _anim.SetBool(HashAttack2, false);
        _anim.SetBool(HashAttack3, false);
        _anim.SetBool(HashAttack1Fin, false);
        _anim.SetBool(HashAttack2Fin, false);
        _anim.SetBool(HashAttack3Fin, false);

        bossmover.Attack2move = false;
    }
    
    public void SetAttack2Fin()
    {
        _anim.SetBool(HashAttack2Fin, true);
    }
    public void Attack2Start()
    {
        bossmover.Attack2move = true;
    }
    public void MoveColliderAttackStart()
    {
        _collider.offset = new Vector2(0, -3);
    }

    public void MoveColliderAttackEnd()
    {
        _collider.offset = new Vector2(0, -2);
    }
    public void MoveColliderAttack2Start()
    {
        _collider.offset = new Vector2(0, 1.4f);
    }
    public void Disappear()
    {
        _collider.offset = new Vector2(0, -10);
    }
}
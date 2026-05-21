using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Member.Kyuwon.SBossSO;

public class BossFinisher : MonoBehaviour
{
    public SBossData bossData;
    [SerializeField] private bool isFacingRight;
    [SerializeField] public List<ParticleGroup> particles;
    private int Skill;
    private Animator _animator;
    [SerializeField] private LayerMask whatIsPlayer;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void FixedUpdate()
    {
        isFacingRight = transform.rotation.y == 0 ? true : false;
    }
    
    public void Finisher()
    {
        {
            Skill = Random.Range(0, 5);

            if (Skill < 3)
            {
                foreach (var main in particles[0].particles)
                {
                    var particle = main.main;
                    particle.startRotationY = isFacingRight ? 0f : 180f * Mathf.Deg2Rad;
                    main?.Play();
                }

                StartCoroutine(FinisherTrigger());
                
                BossMove.instance.MoveSpeed = 0f;
                _animator.SetFloat("MoveX", 0f);
            }
            else
            {
                foreach (var main in particles[0].particles)
                {
                    var particle = main.main;
                    particle.startRotationY = isFacingRight ? 0f : 180f * Mathf.Deg2Rad;
                    main?.Play();
                }

                StartCoroutine(SFinisherTrigger());
                
                BossMove.instance.MoveSpeed = 0f;
                _animator.SetFloat("MoveX", 0f);
            }
        }
    }

    public void FinisherOverLap()
    {
        Collider2D Hit = Physics2D.OverlapBox(transform.position, bossData.FinisherRange + bossData.FinisherRange, 0,whatIsPlayer);
        
        Hit?.GetComponent<IDamageable>()?.GetDamage(1, gameObject);
        
        Debug.Log("Finisher");
    }
    
    private IEnumerator FinisherTrigger()
    {
        yield return new WaitForSeconds(0.6f);
        _animator.SetTrigger("Finisher");
    }
    
    private IEnumerator SFinisherTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("SFinisher");
    }
}

using System;
using System.Collections;
using Member.KimJoonYoung._01.Scripts.Player;
using MoreMountains.Feedbacks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Min_BossMover : MonoBehaviour
{
        private Rigidbody2D _rb;
        
        [SerializeField] private Min_BossHealth bossHealth;
        [SerializeField] private PlayerController player;
        
        [SerializeField] private float speed;
        [SerializeField] private Vector2 offset;
        
        [SerializeField]private Transform playerTrm;
        [SerializeField] private Vector2 attack2BoxSize;
        [SerializeField] private Vector3 attack2BoxOffset;
        [SerializeField] private LayerMask playerLayer;

        public bool Attack1 { get; private set; }
        public bool Attack2 { get; private set; }
        public bool Attack3 { get; private set; }
        public float MoveX { get; private set; }
        public bool Tp { get; private set; }
        public float LastAttackTime { get; private set; }

        private bool _attack2Cool = false;
        

        private bool IsSkill => Attack1 || Attack2 || Attack3 || Tp;
        
        private Vector2 _moveDir;
        private Vector2 _distance;
        private int _skill;
        private bool _isCheckPlayer;
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _distance = player.transform.position - transform.position;
            _moveDir.x = _distance.x > 0 ? 1f : -1f;
            MoveX = Mathf.Abs(_rb.linearVelocityX);

            if (Mathf.Abs(_distance.x) < 8.0f && !IsSkill && !_attack2Cool)
            {
                Attack2Skill();
            }
            
            if (!IsSkill && LastAttackTime <= 0)
            {
                LastAttackTime = 2f;
                StartCoroutine(StartAttack());
            }

        }
        
        private void Update()
        {
            LastAttackTime -= Time.deltaTime;
            if (_moveDir.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_moveDir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
        }

        private IEnumerator StartAttack()
        {
            int random = Random.Range(0, 2);
            switch (random)
            {
                case 0:
                    yield return StartCoroutine(Attack1Skill());
                    break;
                case 1:
                    yield return StartCoroutine(Attack3Skill());
                    break;
            }
        }

        private IEnumerator Attack1Skill()
        {
            yield return StartCoroutine(TeleportRoutine());
            Attack1 = true;
            yield return new WaitUntil(() => !Attack1);
        }

        private IEnumerator Attack2CoolTime()
        {
            _attack2Cool = true;
            yield return new WaitForSeconds(1f);
            _attack2Cool = false;
        }

        private void Attack2Skill()
        {
            Attack2 = true;
            _rb.linearVelocityX = _moveDir.x * speed;
            MoveX = Mathf.Abs(_moveDir.x);
            StartCoroutine(Attack2CoolTime());
        }

        private IEnumerator Attack3Skill()
        {
            yield return StartCoroutine(TeleportRoutine());
            Attack3 = true;
            yield return new WaitUntil(() => !Attack3);
        }

        private IEnumerator TeleportRoutine()
        {
            Tp = true;
            yield return new WaitUntil(() => !Tp);
            
        }

        /*private void Teleport()
        {
            Tp = true;
        }*/

        public void MoveToPlayer()
        {
            transform.position = new Vector2(playerTrm.position.x, transform.position.y);
        }

        public void SkillOff()
        {
            Attack1 = false;
            Attack2 = false;
            Attack3 = false;
            Tp = false;
        }


        /*private void Start()
        {
            StartCoroutine(AttackRoutine());
        }
    
        private void Update()
        {
            
            if (!IsSkill) // 회전
            {
                if (_moveDir.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (_moveDir.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            
            if (Attack1)
            {
                Vector3 currentPos = transform.position;
                transform.position = new Vector3(playerTrm.position.x, currentPos.y, currentPos.z);
                Attack1 = false;
                
            }
            
            if(Attack2)
                _rb.linearVelocityX = _moveDir.x * speed;
            else if (Attack2 == false)
            {
                _rb.linearVelocityX = Vector2.zero.x;
            }
    
            if (Attack3)
            {
                Vector3 currentPos = transform.position;
                transform.position = new Vector3(playerTrm.position.x, currentPos.y, currentPos.z);
                Attack3 = false;
            }
            
            _moveDir = playerTrm.position-transform.position;
            _moveDir.Normalize();
            
        }
        private IEnumerator AttackRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                _skill = Random.Range(1, 3);

                if (_isCheckPlayer) _skill = 3;

                switch (_skill)
                {
                    case 1:
                        yield return StartCoroutine(DoAttack1());
                        break;
                    case 2: 
                        yield return StartCoroutine(DoAttack2()); 
                        break;
                    case 3: 
                        yield return StartCoroutine(DoAttack3());   
                        break;
                }
            
                if (bossHealth.IsDeath)
                    break;
            }
        }

        public void AttackFin()
        {
            Attack1 = false;
            Attack2 = false;
            Attack3 = false;
            if(Tp)
            {
                Tp = false;
                transform.position = playerTrm.position;
            }
        }
        
        public void Teleport()
        {
            /*_anim.SetBool(_hashTeleport, true);
            yield return null;
            yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
            _anim.SetBool(_hashTeleportFinish, true);
            if (_skill == 1)
                bossMover.Attack1 = true;
            else if (_skill == 3)
                bossMover.Attack3 = true;
            _anim.SetBool(_hashTeleport, false);
            yield return null;
            yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
            _anim.SetBool(_hashTeleportFinish, false);#1#
        }
        
        private IEnumerator DoAttack1()
        {
            Attack1 = true;
            yield return null;
            /*_anim.SetBool(_hashAttack1, true);
            yield return null;
            yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
            bossMover.Attack1 = false;
            _anim.SetBool(_hashAttack1, false);
            _anim.SetBool(_hashAttack1Fin, true);
            yield return null;
            _anim.SetBool(_hashAttack1Fin, false);#1#
        }

    
        private IEnumerator DoAttack2()
        {
            Attack2 = true;
            yield return null;
            /*_anim.SetBool(_hashAttack2, true);
            yield return null;
            yield return new WaitUntil(() => _anim.GetBool(_hashAttack2Fin));
            bossMover.Attack2 = false;
            _anim.SetBool(_hashAttack2, false);
            _anim.SetBool(_hashAttack2Fin, false);#1#
        }

        private IEnumerator DoAttack3()
        {
            Attack3 = true;
            yield return null;
            /*_anim.SetBool(_hashAttack3, true);
            yield return null;
            yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
            bossMover.Attack3 = false;
            _anim.SetBool(_hashAttack3, false);
            _anim.SetBool(_hashAttack3Fin, true);
            yield return null;
            _anim.SetBool(_hashAttack3Fin, false);#1#
        }*/
        
        
        
        // 규원심이 싼 똥
        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + (Vector3)attack2BoxOffset, attack2BoxSize);
        }
        
        
        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("트리거 감지됨: " + other.gameObject.name);
        }

        public void Attack1OverLap()
        {
            //Collider2D hit = Physics2D.OverlapBox(transform.position + _Attack2Boxoffset, _Attack2Boxsize, 0f, _playerlayer);
            Debug.Log("Yaho!");
            //hit?.GetComponent<IDamageable>().GetDamage(1, gameObject);
        }
        
        public void Attack2OverLap()
        {
            //Collider2D hit = Physics2D.OverlapBox(transform.position + _Attack2Boxoffset, _Attack2Boxsize, 0f, _playerlayer);
            Debug.Log("Oh Yeah!");
            //hit?.GetComponent<IDamageable>().GetDamage(1, gameObject);
        }
        
        public void Attack3OverLap()
        {
            //Collider2D hit = Physics2D.OverlapBox(transform.position + _Attack2Boxoffset, _Attack2Boxsize, 0f, _playerlayer);
            Debug.Log("HiYa!");
            //hit?.GetComponent<IDamageable>().GetDamage(1, gameObject);
        }*/
}

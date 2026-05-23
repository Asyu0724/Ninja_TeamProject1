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
        

        private bool IsSkill => Attack1 || Attack3 || Tp;
        
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
            
            if (_moveDir.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_moveDir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (Mathf.Abs(_distance.x) <= 7.0f && !IsSkill && !_attack2Cool)
            {
                Attack2Skill();
            }
            
            if (Mathf.Abs(_distance.x) > 7.0f && !IsSkill && LastAttackTime <= 0)
            {
                LastAttackTime = 2f;
                StartCoroutine(StartAttack());
            }

        }
        
        private void Update()
        {
            LastAttackTime -= Time.deltaTime;
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
            _rb.linearVelocity = Vector2.zero;
            Attack3 = false;
            Tp = false;
        }
}

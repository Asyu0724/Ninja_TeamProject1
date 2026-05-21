using MoreMountains.Feedbacks;
using UnityEngine;

public class Min_BossMover : MonoBehaviour
{
        private Rigidbody2D _rb;
        [SerializeField] private float speed = 10f;
        private Vector2 moveDir;
        [SerializeField]private Vector2 offset;
        private Vector2 currentPos;
        
        [SerializeField]private Transform playerTrm;
        [SerializeField] private Vector2 _Attack2Boxsize;
        [SerializeField]private Vector3 _Attack2Boxoffset;
        [SerializeField] private LayerMask _playerlayer;
        [SerializeField] private Min_BossHealth BossHealth;
        
        public bool Attack2move;
        public bool attack1start;
        public bool attack3start;
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            BossHealth = GetComponent<Min_BossHealth>();
        }
    
        private void Update()
        {
            if (attack1start == true)
            {
                Vector3 currentPos = transform.position;
                transform.position = new Vector3(playerTrm.position.x, currentPos.y, currentPos.z);
                attack1start = false;
            }
    
            if (attack3start == true)
            {
                Vector3 currentPos = transform.position;
                transform.position = new Vector3(playerTrm.position.x, currentPos.y, currentPos.z);
                attack3start = false;
            }
            moveDir = playerTrm.position-transform.position;
            moveDir.Normalize();
            if(Attack2move == true)
                _rb.linearVelocityX = moveDir.x * speed;
            else if (Attack2move == false)
            {
                _rb.linearVelocityX = Vector2.zero.x;
            }

            if (BossHealth.IsDeath == true)
            {
                if (Attack2move == true)
                {
                    Attack2move = false;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + (Vector3)_Attack2Boxoffset, _Attack2Boxsize);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("트리거 감지됨: " + other.gameObject.name);
        }

        public void Attack1OverLap()
        {
            /*Collider2D hit = Physics2D.OverlapBox(transform.position + _Attack2Boxoffset, _Attack2Boxsize, 0f, _playerlayer);*/
            Debug.Log("Yaho!");
            /*hit?.GetComponent<IDamageable>().GetDamage(1, gameObject);*/
        }
        
        public void Attack2OverLap()
        {
            /*Collider2D hit = Physics2D.OverlapBox(transform.position + _Attack2Boxoffset, _Attack2Boxsize, 0f, _playerlayer);*/
            Debug.Log("Oh Yeah!");
            //hit?.GetComponent<IDamageable>().GetDamage(1, gameObject);
        }
        
        public void Attack3OverLap()
        {
            //Collider2D hit = Physics2D.OverlapBox(transform.position + _Attack2Boxoffset, _Attack2Boxsize, 0f, _playerlayer);
            Debug.Log("HiYa!");
            //hit?.GetComponent<IDamageable>().GetDamage(1, gameObject);
        }
}

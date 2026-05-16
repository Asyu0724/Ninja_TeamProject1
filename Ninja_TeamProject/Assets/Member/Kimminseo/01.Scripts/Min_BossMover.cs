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
        private Vector2 minLimit;
        private Vector2 maxLimit;
        [SerializeField] private Min_BossRenderer bossrenderer;
        public bool Attack2move;
    
        public bool attack1start{ private get; set; }
        public bool attack3start{ private get; set; }
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            bossrenderer = GetComponent<Min_BossRenderer>();
        }
        private void Start()
        {
            minLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            maxLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
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
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + (Vector3)_Attack2Boxoffset, _Attack2Boxsize);
        }
}

using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 9f;
    [SerializeField]private Vector2 minLimit;
    [SerializeField]private Vector2 maxLimit;
    private float offset = 0.5f;
    private Vector2 currentPos;
    private Vector2 moveDir;
    [SerializeField]private Transform playerTrm;
    private Vector2 boxsize = new Vector2(2f,1f);
    private LayerMask playerLayer;
    public bool press;
    [SerializeField] Vector2 boxcentermanager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        minLimit = Camera.main.ViewportToWorldPoint(new Vector2(0.4f, 0.4f));
        maxLimit = Camera.main.ViewportToWorldPoint(new Vector2(0.7f, 0.7f));
    }
    private void Update()
    {
        Vector2 boxcenter = (Vector2)transform.position + boxcentermanager;
        press = Physics2D.OverlapBox(point:boxcenter, boxsize, angle: 0, playerLayer);
        _rb.linearVelocity = moveDir * speed;
    }
    private void FixedUpdate()
    {
        moveDir = playerTrm.position - transform.position;
        moveDir.Normalize();
    }
    private void LateUpdate()
    {
        currentPos.x = Mathf.Clamp(transform.position.x, minLimit.x + offset, maxLimit.x - offset);
        currentPos.y = Mathf.Clamp(transform.position.y, minLimit.y + offset, maxLimit.y - offset);
        transform.position = currentPos;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector2 boxCenter = (Vector2)transform.position + (Vector2.right * offset);
        Gizmos.DrawWireCube(boxCenter, boxsize);
    }
}

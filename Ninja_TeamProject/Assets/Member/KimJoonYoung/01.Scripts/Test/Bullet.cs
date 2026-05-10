using Member.KimJoonYoung._01.Scripts.Agent;
using Member.KimJoonYoung._01.Scripts.Interface;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            _rb.linearVelocityX = speed;
        }
        else
        {
            _rb.linearVelocityX = -speed;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            if (!player.PlayerHit)
            {
                Destroy(gameObject);    
                other.gameObject.GetComponent<HealthSystem>().GetDamage(1, gameObject);
            }
            
        }
    }
}

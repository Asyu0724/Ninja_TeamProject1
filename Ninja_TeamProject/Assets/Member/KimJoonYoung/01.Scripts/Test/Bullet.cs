using Member.KimJoonYoung._01.Scripts.Interface;
using UnityEngine;

public class Bullet : MonoBehaviour , ICollisionAttackable
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (!player.gameObject.GetComponent<PlayerAttackManager>()._qSkillUse)
                collision.gameObject.GetComponent<HealthSystem>().GetDamage(1, gameObject);
        }


        Destroy(gameObject);
    }
}

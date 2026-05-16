using Member.KimJoonYoung._01.Scripts.Player;
using UnityEngine;

public class StoneUp : MonoBehaviour
{
    Rigidbody2D _rb;
    PlayerController player;
    float minY;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        minY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.GetDamage(1 , gameObject);
        }
    }

    void FixedUpdate()
    {
        if (_rb.position.y <= minY && _rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
            _rb.position = new Vector2(_rb.position.x, minY);
        }
    }

    public void StoneForce()
    {
        _rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
}
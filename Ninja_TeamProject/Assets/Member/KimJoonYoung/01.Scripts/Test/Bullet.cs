using System.Collections;
using Member.KimJoonYoung._01.Scripts.Player;
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
        StartCoroutine(LifeCycle());
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            _rb.linearVelocityX = speed;
        }
        else
        {
            _rb.linearVelocityX = -speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
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

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

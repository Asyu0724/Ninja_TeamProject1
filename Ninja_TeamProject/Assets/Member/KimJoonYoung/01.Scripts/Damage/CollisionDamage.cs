using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.GetDamage(damage, gameObject);
        }
    }
}


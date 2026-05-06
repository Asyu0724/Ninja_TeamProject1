using UnityEngine;

public class BossHitBox : MonoBehaviour , IDamageable
{
    public void GetDamage(int damage, GameObject dealer)
    {
        GetComponentInParent<Boss>().TakeDamage(damage);
    }
}

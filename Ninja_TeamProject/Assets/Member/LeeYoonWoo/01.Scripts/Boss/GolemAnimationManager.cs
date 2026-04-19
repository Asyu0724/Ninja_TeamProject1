using UnityEngine;

public class GolemAnimationManage : MonoBehaviour
{
    GolemBoss boss;
    void Start()
    {
        boss = GetComponentInParent<GolemBoss>();
    }

    public void AttackEnd()
    {
        boss.AttackEnd();
    }
}

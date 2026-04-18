using UnityEngine;

public class GolemAnimationManage : MonoBehaviour
{
    GolemBoss boss;
    Transform player;
    void Start()
    {
        boss = GetComponentInParent<GolemBoss>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void AttackEnd()
    {
        boss.AttackEnd();
    }

    public void BigCloudOverLap()
    {
        boss.BigCloudOverLap();
    }

    public void SmallCloudOverLap()
    {
        boss.SmallCloudOverLap();
    }

    public void SpinAttackOverLap()
    {
        boss.SpinAttackOverLap();
    }


}

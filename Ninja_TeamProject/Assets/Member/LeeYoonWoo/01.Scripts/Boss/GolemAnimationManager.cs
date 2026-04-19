using UnityEngine;

public class GolemAnimationManage : MonoBehaviour
{
    GolemBoss boss;
    Transform player;
    CameraShake cs;
    void Start()
    {
        boss = GetComponentInParent<GolemBoss>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
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

    public void CameraShake()
    {
        cs.Shake(0.1f, 0.1f);
    }
}

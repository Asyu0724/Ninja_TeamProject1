using UnityEngine;

public class GolemAnimationManage : MonoBehaviour
{
    GolemBoss boss;
    PlayerController player;
    CameraShake cs;
    GameObject[] stones;
    StoneUp[] su;
    private int _i = 0;
    void Start()
    {
        boss = GetComponentInParent<GolemBoss>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        stones = GameObject.FindGameObjectsWithTag("Stone");
        su = new StoneUp[stones.Length];
        foreach (GameObject t in stones)
        {
            su[_i] = t.GetComponent<StoneUp>();
            _i++;
        }
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
        foreach (StoneUp t in su)
        {
            t.StoneForce();
        }
    }
}

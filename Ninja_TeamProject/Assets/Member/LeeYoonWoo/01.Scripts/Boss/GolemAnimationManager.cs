using Member.KimJoonYoung._01.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

public class GolemAnimationManage : MonoBehaviour
{
    GolemBoss boss;
    PlayerController player;
    CameraShake cs;
    GameObject[] stones;
    StoneUp[] su;
    private int _i = 0;
    public UnityEvent OnRealDead;
    
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

    public void DieEvent()
    {
        OnRealDead?.Invoke();
    }

    public void CameraShake()
    {
        cs.Shake(0.1f, 0.1f);
        foreach (StoneUp t in su)
        {
            t.StoneForce();
        }
    }
    
    public void StartSFX(string SFX)
    {
        string[] split = SFX.Split(',');
        int sfx = int.Parse(split[0]);
        int ch = int.Parse(split[1]);
        var currentSfx = (AudioManager.Sfx)sfx;
        AudioManager.instance.PlaySfx(currentSfx , ch);
    }
}

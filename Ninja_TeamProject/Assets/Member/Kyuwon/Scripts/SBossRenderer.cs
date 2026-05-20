using System;
using UnityEngine;

public class SBossRenderer : MonoBehaviour
{
    SBoss Sboss;

    private void Awake()
    {
        Sboss = GetComponentInParent<SBoss>();
    }

    public void SlashOverLap()
    {
        Sboss.SlashOverlap();
    }

    public void ChargeOverLap()
    {
        Sboss.ChargeOverLap();
    }

    public void FinisherOverLap()
    {
        Sboss.FinisherOverLap();
    }

    public void ChargeChange()
    {
        Sboss.ChargeChange();
    }
}

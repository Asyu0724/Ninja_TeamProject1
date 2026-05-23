using System;
using Member.KimJoonYoung._01.Scripts.SO;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField] private TimerSO timerSO;
    public int Minute {get; private set;}
    public int Second {get; private set;}
    public float FloatSecond {get; private set;}

    private void OnEnable()
    {
        Minute = timerSO.saveM;
        Second = timerSO.saveS;
        FloatSecond = timerSO.saveF;
    }

    private void Update()
    {
        
        FloatSecond += Time.deltaTime;
        if (FloatSecond >= 0.99f)
        {
            FloatSecond = 0;
            Second += 1;
        }
        if (Second >= 60)
        {
            Second -= 60;
            Minute += 1;
        }
        timerSO.saveM = Minute;
        timerSO.saveS = Second;
        timerSO.saveF = FloatSecond;
    }

    private void OnApplicationQuit()
    {
        timerSO.saveM = 0;
        timerSO.saveS = 0;
        timerSO.saveF = 0;
    }

    public void TimerReset()
    {
        timerSO.saveM = 0;
        timerSO.saveS = 0;
        timerSO.saveF = 0;
    }
}

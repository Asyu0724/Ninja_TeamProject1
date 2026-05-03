using System;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public void OnHit()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
    public void OffHit()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}

using Member.KimJoonYoung._01.Scripts.SO;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    [SerializeField] TimerSO timerSO;
    private int BestM;
    private int BestS;
    private float BestF;

    private void Awake()
    {
        BestM = PlayerPrefs.GetInt("BestM", 0);
        BestS = PlayerPrefs.GetInt("BestS", 0);
        BestF = PlayerPrefs.GetFloat("BestF", 0);
        
        if (timerSO.saveM > BestM && timerSO.saveS > BestS && timerSO.saveF > BestF)
        {
            PlayerPrefs.SetInt("BestM", timerSO.saveM);
            PlayerPrefs.SetInt("BestS", timerSO.saveS);
            PlayerPrefs.SetFloat("BestF", timerSO.saveF);
        }
    }
}

using Member.KimJoonYoung._01.Scripts.SO;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField] private TimerSO timerSO;
    enum Time
    {
        Int , Float
    }

    [SerializeField] private Time time;
    private TextMeshProUGUI _text;
    private int BestM;
    private int BestS;
    private float BestF;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        BestM = PlayerPrefs.GetInt("BestM", 0);
        BestS = PlayerPrefs.GetInt("BestS", 0);
        BestF = PlayerPrefs.GetFloat("BestF", 0);
        if (time == Time.Int)
            _text.SetText($"Best Time : {BestM:D2}:{BestS:D2}");
        if (time == Time.Float)
            _text.SetText($".{(int)(BestF * 100):D2}");
    }

    public void ResetBestTime()
    {
        PlayerPrefs.SetInt("BestM", 0);
        PlayerPrefs.SetInt("BestS", 0);
        PlayerPrefs.SetFloat("BestF", 0);
        if (time == Time.Int)
            _text.SetText($"Best Time : {PlayerPrefs.GetInt("BestM", 0):D2}:{PlayerPrefs.GetInt("BestS", 0):D2}");
        
        if (time == Time.Float)
            _text.SetText($".{(int)(PlayerPrefs.GetFloat("BestF", 0) * 100):D2}");
    }
}

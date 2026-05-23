using System.Collections;
using Member.KimJoonYoung._01.Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextRead : MonoBehaviour
{
    [SerializeField] private TimerSO timer;
    [SerializeField] private string readText;
    [SerializeField] private float readingTime;
    private TextMeshProUGUI _text;
    private string _nowText;
    private WaitForSeconds _wait;
    public UnityEvent OnEnd;

    private void Awake()
    {
        _wait =  new WaitForSeconds(readingTime);
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Read()
    {
        StartCoroutine(OnRead());
    }
    
    private IEnumerator OnRead()
    {
        yield return new WaitForSeconds(2);
        foreach (var txt in readText)
        {
            _nowText += txt.ToString();
            _text.SetText(_nowText);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Tick , 1);
            yield return _wait;
        }
        OnEnd?.Invoke();
    }

    public void TimerRead()
    {
        StartCoroutine(OnTimerRead());
    }

    public void FTimerRead()
    {
        StartCoroutine(OnFTimerRead());
    }

    private IEnumerator OnTimerRead()
    {
        yield return _wait;
        _nowText += $"{timer.saveM:D2}";
        _text.SetText(_nowText);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Majestic, 1);
        yield return _wait;  
        _nowText += $":{timer.saveS:D2}";
        _text.SetText(_nowText);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Majestic, 1);
        OnEnd?.Invoke();
    } 
    private IEnumerator OnFTimerRead()
    {
        yield return _wait;
        _nowText += $".{(int)(timer.saveF * 100):D2}";
        _text.SetText(_nowText);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Majestic, 1);
        OnEnd?.Invoke();
    }
}

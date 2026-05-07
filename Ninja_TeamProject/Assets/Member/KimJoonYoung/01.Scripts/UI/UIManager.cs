using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field:SerializeField] public HealthBarUI HealthUI {  get; private set; }
    [field: SerializeField] public HealthBarUI BossHealthUI { get; private set; }
    [SerializeField] private Image gameOverImage;
    public event Action OnGameOver;

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void Dead()
    {
        gameOverImage.gameObject.SetActive(true);
        StartCoroutine(CoFade());
    }

    IEnumerator CoFade()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 2f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            gameOverImage.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 0.8f, elapsedTime / fadedTime));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnGameOver?.Invoke();
        yield break;
    }
}


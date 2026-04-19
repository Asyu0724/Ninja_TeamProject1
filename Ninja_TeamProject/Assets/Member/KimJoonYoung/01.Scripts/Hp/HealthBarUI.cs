using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;
    private int _heartCount = 0;

    private GameObject[] _hearts;

    RectTransform rect;
    Vector3 originalPos;

    //1.목숨만큼 하트를 자식으로 생성한다.
    public void InitHealthUI(int life)
    {
        _heartCount = life;
        _hearts = new GameObject[_heartCount];

        for (int i = 0; i < life; i++)
        {
            _hearts[i] = Instantiate(_heartPrefab, transform);
        }
    }


    //2.목숨을 잃으면 목숨을 하나씩 비활성화 한다.
    public void UpdateHealthUI(int life)
    {
        for (int i = 0; i < _heartCount; i++)
        {
            if (_heartCount - life > i)
            {
                _hearts[i].SetActive(false);
            }
            else
            {
                _hearts[i].SetActive(true);
            }
        }
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float duration = 0.15f;
        float magnitude = 15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            rect.anchoredPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
    }

}

using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;
    private int _heartCount = 0;

    private GameObject[] _hearts;

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

}

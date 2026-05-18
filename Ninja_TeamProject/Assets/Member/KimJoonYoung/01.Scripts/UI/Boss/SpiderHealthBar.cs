using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI.Boss
{
    public class SpiderHealthBar : MonoBehaviour
    {
        private Slider _slider;
        private float health;
        private float maxHealth;
        
        private void Awake()
        {
            _slider =  GetComponent<Slider>();
        }

        public void InitHealthUI(float health , float maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;
        }

        public void UpdateHealthUI(float health)
        {
            this.health = health;
            StartCoroutine(ValueChange());
        }

        private IEnumerator ValueChange()
        {
            if (_slider.value > health / maxHealth)
            {
                while (_slider.value > health / maxHealth)
                {
                    _slider.value -= Time.deltaTime;
                    yield return null;
                }
            }
            else
                while (_slider.value < health / maxHealth)
                {
                    _slider.value += Time.deltaTime;
                    yield return null;
                }
        }
    }
}

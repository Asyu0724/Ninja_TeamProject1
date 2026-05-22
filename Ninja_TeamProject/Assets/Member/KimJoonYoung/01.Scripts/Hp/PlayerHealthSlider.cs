using System;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.Hp
{
    public class PlayerHealthSlider : MonoBehaviour
    {
        private Slider _healthSlider;

        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
        }
        
        public void InitHealthUI(float maxHealth)
        {
            _healthSlider.value = maxHealth;
        }


        //2.목숨을 잃으면 목숨을 하나씩 비활성화 한다.
        public void UpdateHealthUI(float health , float maxHealth)
        {
            _healthSlider.value = health / maxHealth;
        }
    }
}

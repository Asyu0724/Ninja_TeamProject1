using Member.KimJoonYoung._01.Scripts.Effect;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI.PauseMenu
{
    public class ToggleDataSave : MonoBehaviour
    {
        [SerializeField] private string prefsKey;
        private Toggle _toggle;
        public int Value { get; private set; }

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            Value = PlayerPrefs.GetInt(prefsKey);
            if (Value == 1)
            {
                _toggle.isOn = true;
                PlayerPrefs.SetInt(prefsKey , 1);
            }
            else
            {
                _toggle.isOn = false;
                PlayerPrefs.SetInt(prefsKey , -1);
            }
        }
        
        
        private void OnDisable()
        {
            if (_toggle.isOn)
                PlayerPrefs.SetInt(prefsKey , 1);
            else
                PlayerPrefs.SetInt(prefsKey, -1);
        }
    }
}
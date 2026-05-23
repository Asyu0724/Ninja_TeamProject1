using Member.KimJoonYoung._01.Scripts.SO;
using TMPro;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.UI
{
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private TimerSO timerSo;
        private enum Time
        {
            Int , Float
        }
        [SerializeField] private Time Select;
    
        private TextMeshProUGUI _timerText;

        private void Awake()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (Select == Time.Int)
                _timerText.SetText($"{timerSo.saveM:D2}:{timerSo.saveS:D2}");
            if (Select == Time.Float)
                _timerText.SetText($".{(int)(timerSo.saveF * 100):D2}");
        }
    }
}

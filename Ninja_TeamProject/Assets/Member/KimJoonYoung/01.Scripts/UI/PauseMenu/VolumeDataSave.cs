using UnityEngine;
using UnityEngine.UI;

namespace Member.KimJoonYoung._01.Scripts.UI.PauseMenu
{
    public class VolumeDataSave : MonoBehaviour
    {
        [SerializeField] private string prefsKey;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _slider.value = PlayerPrefs.GetFloat(prefsKey , 1);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(prefsKey , _slider.value);
        }
    }
}

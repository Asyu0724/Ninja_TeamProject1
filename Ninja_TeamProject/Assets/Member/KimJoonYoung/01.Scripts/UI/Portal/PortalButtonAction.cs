using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.KimJoonYoung._01.Scripts.UI.Portal
{
    public class PortalButtonAction : MonoBehaviour
    {
        public event Action OnButtonPress;
        public static PortalButtonAction Instance;

        private void Awake()
        {
            Instance = this;
        }


        private void Update()
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Crack , 14);
                AudioManager.instance.StopBgm();
                OnButtonPress?.Invoke();
                gameObject.SetActive(false);
            }
        }

    }
}

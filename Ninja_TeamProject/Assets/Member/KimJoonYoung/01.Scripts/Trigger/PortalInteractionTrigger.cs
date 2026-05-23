using Member.KimJoonYoung._01.Scripts.Interface;
using Member.KimJoonYoung._01.Scripts.UI.Portal;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Trigger
{
    public class PortalInteractionTrigger : MonoBehaviour , ITriggerable
    {
        private Canvas _btn;
        private bool _canTrigger = true;

        private void Awake()
        {
            _btn = GetComponentInChildren<Canvas>();
        }

        private void Start()
        {
            _btn.gameObject.SetActive(false);
            PortalButtonAction.Instance.OnButtonPress += HandlerDisableThis;
        }

        private void HandlerDisableThis()
        {
            _canTrigger = false;
        }

        public void OnTrigger(GameObject who)
        {
            if (_canTrigger)
            {
                _btn.gameObject.SetActive(true);
            }
        }

        public void OffTrigger(GameObject who)
        {
            if(_canTrigger)
                _btn.gameObject.SetActive(false);
        }
    }
}

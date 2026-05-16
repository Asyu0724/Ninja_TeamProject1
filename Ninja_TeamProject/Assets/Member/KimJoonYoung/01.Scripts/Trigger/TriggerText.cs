using DG.Tweening;
using Member.KimJoonYoung._01.Scripts.Interface;
using TMPro;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Trigger
{
    public class TriggerText : MonoBehaviour , ITriggerable
    {
        [SerializeField] private TextMeshProUGUI text;
        public void OnTrigger(GameObject who = null)
        {
            text.DOFade(1, 2).SetEase(Ease.Linear);
        }

        public void OffTrigger(GameObject who = null)
        {
            text.DOFade(0, 2).SetEase(Ease.OutCubic);
        }
    }
}
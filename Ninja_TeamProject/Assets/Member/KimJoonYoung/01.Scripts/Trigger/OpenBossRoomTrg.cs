using System;
using Member.KimJoonYoung._01.Scripts.Interface;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Trigger
{
    public class OpenBossRoomTrg : MonoBehaviour , ITriggerable
    {
        public event Action OnTrigger;

        
        
        public void Trigger()
        {
            OnTrigger?.Invoke();
        }
    }
}

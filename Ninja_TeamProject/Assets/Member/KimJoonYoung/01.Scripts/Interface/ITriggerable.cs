using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Interface
{
    public interface ITriggerable
    {
        void OnTrigger(GameObject who = null); 
        void OffTrigger(GameObject who = null);
    }
}
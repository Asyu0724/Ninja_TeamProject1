using System;

namespace Member.KimJoonYoung._01.Scripts.Interface
{
    public interface ITriggerable
    {
        event Action OnTrigger;

        void Trigger();
    }
}
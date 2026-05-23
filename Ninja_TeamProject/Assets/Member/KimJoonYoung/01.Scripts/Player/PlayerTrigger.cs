using Member.KimJoonYoung._01.Scripts.Interface;
using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Player
{
    public class PlayerTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out ITriggerable trigger))
                trigger?.OnTrigger();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out ITriggerable trigger))
                trigger?.OffTrigger();
        }
    }
}
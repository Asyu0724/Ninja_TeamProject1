using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Manager
{
    public class TimeScaleManager : MonoBehaviour
    {
        public static TimeScaleManager Instance;
        [SerializeField] private float onHitTimeScale = 0.5f;
        private bool _isPaused;
    
        private void Start()
        {
            Instance = this;
        }

        public void OnHit()
        {
            Time.timeScale = onHitTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    
        public void OffHit()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }

        public void TimeStop()
        {
            Time.timeScale = 0;
        }

        public void TimeResume()
        {
            Time.timeScale = 1;
        }
    }
}

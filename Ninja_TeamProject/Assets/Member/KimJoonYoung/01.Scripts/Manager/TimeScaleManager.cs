using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.Manager
{
    public class TimeScaleManager : MonoBehaviour
    {
        public static TimeScaleManager Instance;
        private bool _isPaused;
    
        private void Start()
        {
            Instance = this;
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

using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.SO
{
    [CreateAssetMenu(fileName = "time data", menuName = "GameData/time data", order = 0)]
    public class TimerSO : ScriptableObject
    {
        public int saveM;
        public int saveS;
        public float saveF;
    }
}
using UnityEngine;

namespace Member.Kyuwon.SBossSO
{
    [CreateAssetMenu(fileName = "SBossSO", menuName = "SBossSO")]
    public class SBossData : ScriptableObject
    {
        public int MaxHp;
        public int CurrentHp;
        public int NormalDamage;
        public int CriticalDamage;
        public float NormalCool;
        public Vector2 NormalRange;
        public Vector2 FinisherRange;
        public bool CanNormal;
        public float ChargingCool;
        public bool CanCharging;
        public float FinisherCool;
        public bool CanFinisher;
        public float TelCool;
        public bool CanTel;
    }
}

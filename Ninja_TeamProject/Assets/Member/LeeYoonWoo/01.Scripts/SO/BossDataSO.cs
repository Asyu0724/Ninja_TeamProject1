using UnityEngine;

namespace Member.LeeYoonWoo.SO
{
    [CreateAssetMenu(fileName = "Boss Damage data", menuName = "BossSO/Boss Damage data", order = 0)]
    public class BossDataSO : ScriptableObject
    {
        public int maxHealth;
        public int bigCloudDamage;
        public int smallCloudDamage;
        public int spinAttackDamage;
    }
}
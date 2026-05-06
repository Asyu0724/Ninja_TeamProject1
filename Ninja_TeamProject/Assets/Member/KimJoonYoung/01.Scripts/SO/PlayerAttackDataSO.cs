using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.SO
{
    [CreateAssetMenu(fileName = "player Attack data", menuName = "PlayerSO/player Attack data", order = 0)]
    public class PlayerAttackDataSO : ScriptableObject
    {
        public int attackDamageAmount;
        public float canComboAttackTimer;
        public float canAttackTimer;
    }
}
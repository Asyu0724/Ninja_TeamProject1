using UnityEngine;

namespace Member.KimJoonYoung._01.Scripts.SO
{
    [CreateAssetMenu(fileName = "Player Skill data", menuName = "PlayerSO/Player Skill data", order = 0)]
    public class PlayerSkillDataSO : ScriptableObject
    {
        public float skillCoolTime;
        public int skillDamageAmount;
        public Vector2 skillBoxSize;
        public Vector2 skillBoxOffset;
    }
}
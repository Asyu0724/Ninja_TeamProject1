using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillBarUI : MonoBehaviour
{
    [SerializeField] private Slider skillBar;
    private PlayerSkill playerSkill;

    private void Awake()
    {
        playerSkill = GetComponentInParent<PlayerSkill>();
        skillBar.maxValue = playerSkill._qskillCoolTime;
        skillBar.value = 0;
    }

    public void QSkillCoolTimeBarUpdate()
    {
        skillBar.value = skillBar.maxValue;
    }

    private void Update()
    {
        if (skillBar.value >= 0)
        {
            skillBar.value -= Time.deltaTime;
        }
    }
}

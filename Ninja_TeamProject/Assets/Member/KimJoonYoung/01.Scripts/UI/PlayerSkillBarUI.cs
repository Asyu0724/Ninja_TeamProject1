using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillBarUI : MonoBehaviour
{
    [SerializeField] private Slider skillBar;
    private PlayerAttackManager _playerAttackManager;

    private void Awake()
    {
        _playerAttackManager = GetComponentInParent<PlayerAttackManager>();
        skillBar.maxValue = _playerAttackManager._qskillCoolTime;
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

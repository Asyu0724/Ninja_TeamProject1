using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCoolTimebarUI : MonoBehaviour
{
    [SerializeField] private Slider skillBar;
    private PlayerController playerController;
    private float _qSkillCooltime;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        skillBar.maxValue = playerController._qskillCoolTime;
        skillBar.value = 0;
        _qSkillCooltime = playerController._qskillCoolTime;
    }

    public void qSkillCoolTimeBarUpdate()
    {
        skillBar.value = 0;
        while (skillBar.value >= skillBar.maxValue)
        {
            skillBar.value += Time.deltaTime;
        }
    }
}

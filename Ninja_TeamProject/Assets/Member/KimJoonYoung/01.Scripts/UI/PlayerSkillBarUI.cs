using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillBarUI : MonoBehaviour
{
    [SerializeField] private Slider skillBar;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        skillBar.maxValue = playerController._qskillCoolTime;
        skillBar.value = skillBar.maxValue;
    }

    public void QSkillCoolTimeBarUpdate()
    {
        skillBar.value = 0;
    }

    private void Update()
    {
        if (skillBar.value <= skillBar.maxValue)
        {
            skillBar.value += Time.deltaTime;
        }
    }
}

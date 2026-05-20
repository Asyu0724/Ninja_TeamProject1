using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider bossHealthBar;

    public void UpdateBossHealthBar()
    {
        bossHealthBar.value = BossHealthSystem.instance.health / (float)BossHealthSystem.instance.maxHealth;
    }
}

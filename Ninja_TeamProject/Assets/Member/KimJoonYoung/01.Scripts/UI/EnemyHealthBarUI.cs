using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponentInParent<HealthSystem>();
    }

    private void Start()
    {
        _healthBar.maxValue = _healthSystem.Health;
        _healthBar.value = _healthSystem.Health;
    }

    public void HealthBarUpdate()
    {
        _healthBar.value = _healthSystem.Health;
    }
}

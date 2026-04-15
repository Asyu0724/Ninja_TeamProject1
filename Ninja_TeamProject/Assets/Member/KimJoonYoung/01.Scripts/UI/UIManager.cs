using UnityEngine;

public class UIManager : MonoBehaviour
{
/*    [field:SerializeField]
    public GameObject ScoreUI {  get; private set; }
    [field:SerializeField]
    public GameObject GameOverUI {  get; private set; }*/
    [field:SerializeField]
    public HealthBarUI HealthUI {  get; private set; }

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

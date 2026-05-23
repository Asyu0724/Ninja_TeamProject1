using UnityEngine;

public class BgmStarting : MonoBehaviour
{
    [SerializeField] private AudioManager.Bgm bgm;
    void Start()
    {
        AudioManager.instance.PlayBgm(bgm , true);
    }
}

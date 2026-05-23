using UnityEngine;

public class BgmStarting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.main , true);
    }
}

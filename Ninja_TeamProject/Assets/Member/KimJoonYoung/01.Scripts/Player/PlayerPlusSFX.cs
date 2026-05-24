using UnityEngine;

public class PlayerPlusSFX : MonoBehaviour
{   
    public void StartSFX(string SFX)
    {
        string[] split = SFX.Split(',');
        int sfx = int.Parse(split[0]);
        int ch = int.Parse(split[1]);
        var currentSfx = (AudioManager.Sfx)sfx;
        AudioManager.instance.PlaySfx(currentSfx , ch);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomManager : MonoBehaviour
{
    public static BloomManager Instance;
    private Volume _volume;
    private bool _fading;
    

    private void Awake()
    {
        Instance = this;
        _volume = GetComponent<Volume>();
    }

    public void OnHit(float health , float maxHealth)
    {
        _volume.profile.TryGet(out Vignette color);
        StartCoroutine(VignetteFadeI(color , health, maxHealth));
    }

    IEnumerator VignetteFadeI(Vignette color, float health, float maxHealth)
    {
        _fading = true;
        float maxIntensity = 0.25f;
        
        if (health > 0)
            color.smoothness.value += (maxHealth % health) / maxHealth;
        else
            color.smoothness.value = 1;
        
        while (color.intensity.value <= maxIntensity)
        {
            color.intensity.value += 0.04f;
            yield return new WaitForSeconds(0.01f);
        }
        _fading = false;
        StartCoroutine(VignetteFadeO(color, health, maxHealth));
        yield return null;
    }

    IEnumerator VignetteFadeO(Vignette color, float health, float maxHealth)
    {
        float minIntensity = 0f;
        
        if (health > 0)
        {
            yield return new WaitForSeconds(0.5f);
            while (color.intensity.value >= minIntensity && !_fading)
            {
                color.intensity.value -= 0.01f;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}

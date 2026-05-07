using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomManager : MonoBehaviour
{
    public static BloomManager Instance;
    private Volume _volume;
    

    private void Awake()
    {
        Instance = this;
        _volume = GetComponent<Volume>();
    }

    public void OnHit()
    {
        _volume.profile.TryGet(out Vignette color);
        color.color.value = Color.red;
    }   
    
    public void OffHit(float health , float maxHealth)
    {
        _volume.profile.TryGet(out Vignette color);
        
        if (health / maxHealth <= 0.4f)
        {
            color.color.value = Color.softRed;
        }
        else if (health / maxHealth <= 0.6f)
        {
            color.color.value = Color.indianRed;
        }
        else
        {
            color.color.value = Color.white;
        }
    }
}

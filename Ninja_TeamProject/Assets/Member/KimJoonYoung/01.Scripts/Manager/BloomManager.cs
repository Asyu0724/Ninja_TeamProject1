using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomManager : MonoBehaviour
{
    public static BloomManager Instance;
    private Volume volume;
    

    private void Awake()
    {
        Instance = this;
        volume = GetComponent<Volume>();
    }

    public void OnHit()
    {
        volume.profile.TryGet(out ColorAdjustments color);
        color.colorFilter.value = Color.red;
        
    }   
    
    public void OffHit()
    {
        volume.profile.TryGet(out ColorAdjustments color);
        color.colorFilter.value = Color.white;
        
    }
}

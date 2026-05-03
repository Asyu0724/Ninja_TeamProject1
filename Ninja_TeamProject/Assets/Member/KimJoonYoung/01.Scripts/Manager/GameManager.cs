using System;
using Unity.Cinemachine;
using UnityEditor.Search;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;
    public TimeScaleManager timeScaleManager;
    public BloomManager bloomManager;
        
    private void Awake()
    {
        Instance = this;
    }
}

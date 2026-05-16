using System;
using Member.KimJoonYoung._01.Scripts.Manager;
using Member.KimJoonYoung._01.Scripts.Player;
using Unity.Cinemachine;
using UnityEditor.Search;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;
    public BloomManager bloomManager;
        
    private void Awake()
    {
        Instance = this;
    }
}

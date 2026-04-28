using System;
using Unity.Cinemachine;
using UnityEditor.Search;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;

    private void Awake()
    {
        Instance = this;
    }
}

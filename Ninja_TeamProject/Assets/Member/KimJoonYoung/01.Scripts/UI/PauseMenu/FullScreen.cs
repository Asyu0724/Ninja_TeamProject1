using System;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    [field: SerializeField] public bool IsFullScreen { get; set; } = true;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FullScreen") == 1)
        {
            Screen.fullScreen = true;
            Debug.Log("Full Screen Set");
        }
        else
        {
            Screen.fullScreen = false;
            Debug.Log("!Full Screen Set");
        }
    }

    public void OnFullScreen()
    {
        if (!IsFullScreen)
        {
            IsFullScreen = true;
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("FullScreen", 1);
            Debug.Log("Full Screen Set");
        }
        else
        {           
            IsFullScreen = false;
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("FullScreen" , -1);
            Debug.Log("!Full Screen Set");
        }
    }
}
